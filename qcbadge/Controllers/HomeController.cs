﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace qcbadge.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string s, string id, int refresh)
        {
            Helpers.Sql sql = new Helpers.Sql();
            ViewData["refresh"] = refresh;

            if ((String.Compare(Startup.scode, s, true) == 0))
            {
                ViewData["Message"] = "";
                ViewData["0"] = 0;
                ViewData["1"] = 0;
                ViewData["38"] = 0;

                bool[] imglist = new bool[50];

                if (String.IsNullOrEmpty(id))
                {
                    imglist = sql.selectGlobalView();

                    for (int i = 0; i < 50; i++)
                    {
                        ViewData[i.ToString()] = imglist[i];
                    }
                }
                else
                {
                    int badgeid = Convert.ToInt32(id);
                    badgeid = badgeid - 1;
                    imglist = sql.selectIndervidualView(badgeid);

                    for (int i = 0; i < 50; i++)
                    {
                        ViewData[i.ToString()] = imglist[i];
                    }

                }

                return View();
            }
            else { return StatusCode(401); }

        }

        public IActionResult List(string s, string id, int refresh)
        {
            Helpers.Sql sql = new Helpers.Sql();
            ViewData["refresh"] = refresh;

            if ((String.Compare(Startup.scode, s, true) == 0))
            {
                ViewData["Message"] = "";
                ViewData["0"] = 0;
                ViewData["1"] = 0;
                ViewData["38"] = 0;

                bool[] imglist = new bool[50];

                if (String.IsNullOrEmpty(id))
                {
                    imglist = sql.selectGlobalView();

                    for (int i = 0; i < 50; i++)
                    {
                        ViewData[i.ToString()] = imglist[i];
                    }
                }
                else
                {
                    int badgeid = Convert.ToInt32(id);
                    badgeid = badgeid - 1;
                    imglist = sql.selectIndervidualView(badgeid);

                    for (int i = 0; i < 50; i++)
                    {
                        ViewData[i.ToString()] = imglist[i];
                    }

                }

                return View();
            }
            else { return StatusCode(401); }

        }

        public IActionResult Update(string advertData, string advertData64)
        {

            //// Flags; this sets the device to use limited discoverable
            //// mode (advertises for 30 seconds at a time) instead of general
            //// discoverable mode (advertises indefinitely)
            //2,   // length of this data
            //GAP_ADTYPE_FLAGS, // 0x01
            //GAP_ADTYPE_FLAGS_BREDR_NOT_SUPPORTED, // 0x04 

            //// Appearance: This is a #badgelife header.
            //3,    // Length of this data
            //GAP_ADTYPE_APPEARANCE, // Data type: "Appearance" // 0x19
            //0xDC, // DC
            //0x19, // 19 #badgelife

            //// Queercon data: ID, current icon, etc
            //15, // length of this data including the data type byte
            //GAP_ADTYPE_MANUFACTURER_SPECIFIC, // manufacturer specific adv data type // 0xff
            //0xD3, // Company ID - Fixed (queercon)
            //0x04, // Company ID - Fixed (queercon)
            //0x00, // Badge ID MSB
            //0x00, // Badge ID LSB
            //0x00, // Current icon ID
            //0x00, // icon 40..47
            //0x00, // icon 32..39
            //0x00, // icon 24..31
            //0x00, // icon 16..23
            //0x00, // icon  8..15
            //0x00, // icon  0.. 7
            //0x00, // CHECK

            //9,
            //GAP_ADTYPE_LOCAL_NAME_SHORT, // 0x08
            //0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

            //     Jakes take on the header:
            //     0x0201040319DC190FFFD304 < -Fixed header
            //     [AAAA] < -Badge ID 0 - 289 In Dec / 0000 - 0121 in Hex
            //     [BB] < -Current Icon
            //     //NOPE NOT ANYMORE//[CC] < -RESERVED(incase jonathan wants more than 40 icons ?)
            //     [CCDDDDDDDDDD] < -icon bit array 47...........0
            //     [EE] < -Checksum
            //     0908[41524F5947424956] < -End + Crypto
            //
            //     0x0201040319DC190FFFD304AAAABBCCDDDDDDDDDDEE090841524F5947424956
            //     0x0201040319DC190FFFD3040122BBCCDDDDDDDDDDEE090841524F5947424956 = Badgeid = 122/290
            //     http://localhost:55091/Home/Update?advertdata64=AgEEAxncGQ//0wQBIrvM3d3d3d3uCQhBUk9ZR0JJVg==

            if (String.IsNullOrEmpty(advertData) && String.IsNullOrEmpty(advertData64))
            {

                return StatusCode(200);

            }
            else
            {

                if(!String.IsNullOrEmpty(advertData64))
                {
                    byte[] bytes = Convert.FromBase64String(advertData64);
                    advertData = "0x" + BitConverter.ToString(bytes);

                    var charsToRemove = new string[] { "-" };
                    foreach (var c in charsToRemove)
                    {
                        advertData = advertData.Replace(c, string.Empty);
                    }

                    System.Diagnostics.Debug.WriteLine(advertData);
                }

                String header = "0x0201040319DC190FFFD304";
                String footer = "090841524F5947424956";

                if(advertData.StartsWith(header) && advertData.EndsWith(footer))
                {

                    //http://tomeko.net/online_tools/hex_to_base64.php?lang=en

                    //for base64 convert
                    

                    String qcData = advertData.Substring(24, 20);
                    System.Diagnostics.Debug.WriteLine(qcData);

                    String badgeIdStr = qcData.Substring(0, 4);
                    System.Diagnostics.Debug.WriteLine(badgeIdStr);
                    int badgeId = Convert.ToInt32(badgeIdStr, 16);
                    System.Diagnostics.Debug.WriteLine(badgeId);

                    String curIconStr = qcData.Substring(4, 2);
                    System.Diagnostics.Debug.WriteLine(curIconStr);
                    int curIcon = Convert.ToInt32(curIconStr, 16);
                    System.Diagnostics.Debug.WriteLine(curIcon);

                    //Need to convert the int to a bit array
                    String curIconArrStr = qcData.Substring(6, 12);
                    System.Diagnostics.Debug.WriteLine(curIconArrStr);
                    long curIconArr = Convert.ToInt64(curIconArrStr, 16);
                    System.Diagnostics.Debug.WriteLine(curIconArr);


                    string binaryArr = Convert.ToString(curIconArr, 2); //Convert to binary in a string

                    int[] bitSet = binaryArr.PadLeft(48, '0') // Add 0's from left
                                 .Select(c => int.Parse(c.ToString())) // convert each char to int
                                 .ToArray(); // Convert IEnumerable from select to Array

                    //Bitset is inversed from spec. LSB==47
                    for (int i = 0; i < 48; i++)
                    {
                        System.Diagnostics.Debug.WriteLine("i: " + i);
                        System.Diagnostics.Debug.WriteLine(bitSet[i]);
                    }

                    return StatusCode(200);

                }
                else
                {
                    return StatusCode(400);
                }

                
            }

        }
        public IActionResult Error()
        {
            return View();
        }

        private bool IsBitSet(long b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reWZ;
using reWZ.WZProperties;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WzStringExtractor
{
    class ExtractDamageSkinNumbers
    {
        public ExtractDamageSkinNumbers(string fileName, string outputLocation)
        {
            int count = 0;
            //var dmgSkins = JsonConvert.DeserializeObject<List<DamageSkin>>(File.ReadAllText(jsonLocation));


            WZFile xz = new WZFile(fileName, WZVariant.MSEA, false);
            WZSubProperty damageSkinNumberImg = (WZSubProperty)xz.MainDirectory["BasicEff.img"]["damageSkin"];
            foreach (var numberType in damageSkinNumberImg)
            {
                Bitmap dmgSkinNumberPng = null;
                //foreach (var numberImg in numberType)
                //{
                //    foreach (var number in numberImg)
                //    {
                //        if (!(number is WZCanvasProperty))
                //        {
                //            break;
                //        }

                //        WZCanvasProperty test = (WZCanvasProperty)number;
                //        string[] pathNames = number.Path.Split('/');
                //        int itemId = 0;

                //        if (numberType.HasChild("ItemID"))
                //        {
                //            itemId = numberType["ItemID"].ValueOrDefault<Int32>(Int32.Parse(pathNames[3]));
                //        }

                //        if (numberType["NoRed0"]["3"].HasChild("_inlink"))
                //        {
                //            break;
                //            //string path = numberType["NoCri1"]["5"]["_inlink"].ValueOrDie<string>();
                //            //dmgSkinNumberPng = xz.ResolvePath($"BasicEff.img/{path}").ValueOrDie<Bitmap>();

                //        }

                //        else
                //        {
                //            dmgSkinNumberPng = numberType["NoRed0"]["3"].ValueOrDie<Bitmap>();
                //        }

                //        //Directory.CreateDirectory($@"{outputLocation}\");
                //        if (numberType.HasChild("ItemID"))
                //        {
                //            dmgSkinNumberPng.Save($@"{outputLocation}\{numberType.Name}_{itemId}.png", ImageFormat.Png);
                //        }
                //        else
                //        {
                //            dmgSkinNumberPng.Save($@"{outputLocation}\{numberType.Name}.png", ImageFormat.Png);

                //        }
                //        Console.WriteLine($"Exported damage skin - {pathNames[3]}");
                //    }
                //}

                if (numberType.HasChild("ItemID"))
                {
                    Console.WriteLine("hi");
                    foreach (var numberImg in numberType)
                    {
                        foreach (var number in numberImg)
                        {
                            if (!(number is WZCanvasProperty))
                            {
                                break;
                            }

                            WZCanvasProperty test = (WZCanvasProperty)number;
                            string[] pathNames = number.Path.Split('/');
                            int itemId = numberType["ItemID"].ValueOrDefault<Int32>(Int32.Parse(pathNames[3]));


                            if (number.HasChild("_inlink"))
                            {
                                break;
                                string path = number["_inlink"].ValueOrDie<string>();
                                dmgSkinNumberPng = xz.ResolvePath($"BasicEff.img/{path}").ValueOrDie<Bitmap>();

                            }
                            else
                            {
                                dmgSkinNumberPng = test.Value;
                            }

                            Directory.CreateDirectory($@"{outputLocation}\{pathNames[3]}_{itemId.ToString()}\{pathNames[4]}");
                            dmgSkinNumberPng.Save($@"{outputLocation}\{pathNames[3]}_{itemId.ToString()}\{pathNames[4]}\{pathNames[5]}.png", ImageFormat.Png);
                            Console.WriteLine("Exported damage skin");
                        }
                    }
                }
                else
                {
                    foreach (var numberImg in numberType)
                    {
                        foreach (var number in numberImg)
                        {
                            if (!(number is WZCanvasProperty))
                            {
                                break;
                            }

                            WZCanvasProperty test = (WZCanvasProperty)number;
                            string[] pathNames = number.Path.Split('/');

                            if (number.HasChild("_inlink"))
                            {
                                string path = number["_inlink"].ValueOrDie<string>();
                                dmgSkinNumberPng = xz.ResolvePath($"BasicEff.img/{path}").ValueOrDie<Bitmap>();

                            }
                            else
                            {
                                dmgSkinNumberPng = test.Value;
                            }

                            Directory.CreateDirectory($@"{outputLocation}\{pathNames[3]}\{pathNames[4]}");
                            dmgSkinNumberPng.Save($@"{outputLocation}\{pathNames[3]}\{pathNames[4]}\{pathNames[5]}.png", ImageFormat.Png);
                            Console.WriteLine($"Exported damage skin - {pathNames[3]}");
                        }
                    }
                }
                count++;
            }
            Console.WriteLine($"Successfully dumped {count.ToString()} number of damage skins");
            Console.ReadKey();
        }
    }
}

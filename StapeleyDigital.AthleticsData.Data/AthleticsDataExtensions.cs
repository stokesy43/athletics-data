using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StapeleyDigital.AthelticsData.Domain;

namespace StapeleyDigital.AthleticsData.Data
{
    public static class AthleticsDataExtensions
    {
        public static void EnsureSeedDataForContext(this AthleticsDataContext context)
        {
            if (!context.Standards.Any())
            {
                // init seed data
                var standards = new List<Standard>()
                {
                    new Standard()
                    {
                        Name = "Grade 1",
                        GroupName = "AAA Standards",
                        Priority = 1
                    },
                    new Standard()
                    {
                        Name = "Grade 2",
                        GroupName = "AAA Standards",
                        Priority = 2
                    },
                    new Standard()
                    {
                        Name = "Grade 3",
                        GroupName = "AAA Standards",
                        Priority = 3
                    },
                    new Standard()
                    {
                        Name = "Grade 4",
                        GroupName = "AAA Standards",
                        Priority = 4
                    },
                    new Standard()
                    {
                        Name = "Platinum",
                        GroupName = "CNAC",
                        Priority = 1
                    },
                    new Standard()
                    {
                        Name = "Gold",
                        GroupName = "CNAC",
                        Priority = 2
                    },
                    new Standard()
                    {
                        Name = "Silver",
                        GroupName = "CNAC",
                        Priority = 3
                    },
                    new Standard()
                    {
                        Name = "Bronze",
                        GroupName = "CNAC",
                        Priority = 4
                    }
                };

                context.Standards.AddRange(standards);
                context.SaveChanges();

            }

            if (!context.Events.Any())
            {
                var events = new List<Event>()
                {
                    new Event
                    {
                        Name = "60m",
                        PowerOf10Id = "60"
                    },
                    new Event
                    {
                        Name = "75m",
                        PowerOf10Id = "75"
                    },
                    new Event
                    {
                        Name = "100m",
                        PowerOf10Id = "100"
                    },
                     new Event
                    {
                        Name = "150m",
                        PowerOf10Id = "150"
                    },
                    new Event
                    {
                        Name = "200m",
                        PowerOf10Id = "200"
                    },
                    new Event
                    {
                        Name = "300m",
                        PowerOf10Id = "300"
                    },
                    new Event
                    {
                        Name = "400m",
                        PowerOf10Id = "400"
                    },
                    new Event
                    {
                        Name = "600m",
                        PowerOf10Id = "600"
                    },
                    new Event
                    {
                        Name = "800m",
                        PowerOf10Id = "800"
                    },
                    new Event
                    {
                        Name = "1200m",
                        PowerOf10Id = "1200"
                    },
                    new Event
                    {
                        Name = "1500m",
                        PowerOf10Id = "1500"
                    },
                    new Event
                    {
                        Name = "3000m",
                        PowerOf10Id = "3000"
                    },
                    new Event
                    {
                        Name = "60m Hurdles Under 13 Women",
                        PowerOf10Id = "60HU13W",
                        ShortName="60m Hurdles"
                    },
                    new Event
                    {
                        Name = "70m Hurdles Under13 Women",
                        PowerOf10Id = "70HU13W",
                        ShortName="70m Hurdles"
                    },
                    new Event
                    {
                        Name = "60m Hurdles Under 15 Women",
                        PowerOf10Id = "60HU15W",
                        ShortName="60m Hurdles"
                    },
                    new Event
                    {
                        Name = "75m Hurdles U15 Women",
                        PowerOf10Id = "75HU15W",
                        ShortName = "75m Hurdles"
                    },
                    new Event
                    {
                        Name = "60m Hurdles U17 Women",
                        PowerOf10Id = "60HU17W",
                        ShortName = "60m Hurdles"
                    },
                    new Event
                    {
                        Name = "80m Hurdles U17 Women",
                        PowerOf10Id = "80U17W",
                        ShortName="80m Hurdles"
                    },
                    new Event
                    {
                        Name = "300m Hurdles Women",
                        PowerOf10Id = "300HW",
                        ShortName="300m Hurdles"
                    },
                    new Event
                    {
                        Name = "60m Hurdles Women",
                        PowerOf10Id = "60HW",
                        ShortName = "60m Hurdles"
                    },
                    new Event
                    {
                        Name = "100m Hurdles Women",
                        PowerOf10Id = "100HW",
                        ShortName = "100m Hurdles"
                    },
                    new Event
                    {
                        Name = "400m Hurdles Women",
                        PowerOf10Id = "400HW",
                        ShortName = "400m Hurdles"
                    },
                    new Event
                    {
                        Name = "60m Hurdles Under 13 Men",
                        PowerOf10Id = "60HU13M",
                        ShortName="60m Hurdles"
                    },
                    new Event
                    {
                        Name = "75m Hurdles Under 13 Men",
                        PowerOf10Id = "75HU13M",
                        ShortName="75m Hurdles"
                    },
                   new Event
                    {
                        Name = "60m Hurdles Under 15 Men",
                        PowerOf10Id = "60HU15M",
                        ShortName="60m Hurdles"
                    },
                   new Event
                    {
                        Name = "80m Hurdles Under 15 Men",
                        PowerOf10Id = "80HU15M",
                        ShortName="80m Hurdles"
                    },
                   new Event
                    {
                        Name = "60m Hurdles Under 17 Men",
                        PowerOf10Id = "60HU17M",
                        ShortName="60m Hurdles"
                    },
                   new Event
                    {
                        Name = "100m Hurdles Under 17 Men",
                        PowerOf10Id = "100HU17M",
                        ShortName="100m Hurdles"
                    },
                   new Event
                    {
                        Name = "400m Hurdles Under 17 Men",
                        PowerOf10Id = "400HU17M",
                        ShortName="400m Hurdles"
                    },
                   new Event
                    {
                        Name = "60m Hurdles Under 20 Men",
                        PowerOf10Id = "60HU20M",
                        ShortName="60m Hurdles"
                    },
                   new Event
                    {
                        Name = "110m Hurdles Under 20 Men",
                        PowerOf10Id = "110HU20M",
                        ShortName="110m Hurdles"
                    },
                   new Event
                    {
                        Name = "400m Hurdles Men",
                        PowerOf10Id = "400H",
                        ShortName="400m Hurdles"
                    },
                   new Event
                    {
                        Name = "110m Hurdles Men",
                        PowerOf10Id = "110H",
                        ShortName="110m Hurdles"
                    },
                   new Event
                    {
                        Name = "60m Hurdles Men",
                        PowerOf10Id = "60H",
                        ShortName="60m Hurdles"
                    },
                   new Event
                   {
                       Name= "High Jump",
                       PowerOf10Id="HJ"
                   },
                   new Event
                   {
                       Name="Pole Vault",
                       PowerOf10Id="PV"
                   },
                   new Event
                   {
                       Name="Long Jump",
                       PowerOf10Id="LJ"
                   },
                   new Event
                   {
                       Name="Triple Jump",
                       PowerOf10Id="TJ"
                   },
                   new Event
                   {
                       Name= "Shot Put 3.25kg",
                       PowerOf10Id="SP3.25K",
                       ShortName ="Shot Put"
                   },
                   new Event
                   {
                       Name="Discus 1kg",
                       PowerOf10Id="DT1K",
                       ShortName="Discus"
                   },
                   new Event
                   {
                       Name= "Shot Put 4kg",
                       PowerOf10Id="SP4K",
                       ShortName ="Shot Put"
                   },
                   new Event
                   {
                       Name="Discus 1.25kg",
                       PowerOf10Id="DT1.25K",
                       ShortName="Discus"
                   }                   ,
                   new Event
                   {
                       Name= "Shot Put 5kg",
                       PowerOf10Id="SP5K",
                       ShortName ="Shot Put"
                   },
                   new Event
                   {
                       Name="Discus 1.5kg",
                       PowerOf10Id="DT1.5K",
                       ShortName="Discus"
                   },
                   new Event
                   {
                       Name= "Shot Put 6kg",
                       PowerOf10Id="SP6K",
                       ShortName ="Shot Put"
                   },
                   new Event
                   {
                       Name="Discus 1.75kg",
                       PowerOf10Id="DT1.75K",
                       ShortName="Discus"
                   },
                    new Event
                   {
                       Name= "Shot Put 7.26kg",
                       PowerOf10Id="SP7.26K ",
                       ShortName ="Shot Put"
                   },
                   new Event
                   {
                       Name="Discus 2kg",
                       PowerOf10Id="DT2K",
                       ShortName="Discus"
                   },
                   new Event
                   {
                       Name="Javelin 400g",
                       PowerOf10Id="JT400",
                       ShortName="Javelin"
                   },
                   new Event
                   {
                       Name="Javelin 500g",
                       PowerOf10Id="JT500",
                       ShortName="Javelin"
                   },
                   new Event
                   {
                       Name="Javelin 700g",
                       PowerOf10Id="JT700",
                       ShortName="Javelin"
                   },
                    new Event
                   {
                       Name="Javelin 800g",
                       PowerOf10Id="JT800",
                       ShortName="Javelin"
                   },
                     new Event
                   {
                       Name="Hammer 4kg",
                       PowerOf10Id="HT4K",
                       ShortName="Hammer"
                   },
                       new Event
                   {
                       Name="Hammer 5kg",
                       PowerOf10Id="HT5K",
                       ShortName="Hammer"
                   },
                        new Event
                   {
                       Name="Hammer 6kg",
                       PowerOf10Id="HT6K",
                       ShortName="Hammer"
                   },
                        new Event
                   {
                       Name="Hammer 7.26kg",
                       PowerOf10Id="HT7.26K",
                       ShortName="Hammer"
                   },
                       new Event
                   {
                       Name="Hammer 3kg",
                       PowerOf10Id="HT3K",
                       ShortName="Hammer"
                   }
                };

                context.Events.AddRange(events);
                context.SaveChanges();

            }

        }
    }
}

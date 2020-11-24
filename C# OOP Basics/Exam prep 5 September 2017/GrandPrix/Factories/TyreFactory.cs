using System;
using System.Collections.Generic;
using System.Text;

public class TyreFactory
{
    public Tyre CreateTyre(List<string> tyreInfo)
    {
       if(tyreInfo.Count > 0)
        {
            var tyreType = tyreInfo[0];
            if(tyreInfo.Count > 1)
            {
                var hardness = Double.Parse(tyreInfo[1]);

                switch (tyreType)
                {
                    case "Ultrasoft":
                        if (tyreInfo.Count > 2)
                        {
                            return new UltrasoftTyre(hardness, Double.Parse(tyreInfo[2]));
                        }
                        break;
                    case "Hard":
                        return new HardTyre(hardness);
                }
            }
        }

        throw new ArgumentException("");
    }
}

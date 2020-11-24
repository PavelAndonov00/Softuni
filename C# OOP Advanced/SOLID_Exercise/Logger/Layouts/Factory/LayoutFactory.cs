namespace Logger.Layouts.Factory
{
    using Logger.Layouts.Contracts;
    using Logger.Layouts.Factory.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout(string layoutType)
        {
            switch (layoutType)
            {
                case "SimpleLayout":
                    return new SimpleLayout();
                case "XmlLayout":
                    return new XmlLayout();
                default:
                    throw new ArgumentException("Invalid layout type!");
            }
        }
    }
}

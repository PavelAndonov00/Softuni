namespace Logger.Layouts.Factory.Contracts
{
    using Logger.Layouts.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILayoutFactory
    {
        ILayout CreateLayout(string layoutType);
    }
}

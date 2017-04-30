namespace Ganymede.GanymedeUI
{
    using Communications;
    using System;
    using Logger;

    public static class CommunicationAccessor
    {
        private static CommunicationScope comm = null;

        public static void Initialize(string uriAddress)
        {
            try
            {
                if(comm == null)
                {
                    comm = new CommunicationScope(uriAddress);
                }
            }
            catch(Exception e)
            {
                Logger.Write("Could not create instance of Communication Scope. \n "  + e.InnerException);
            }
        }

        public static CommunicationScope GetCommScope()
        {
            return comm;
        }
    }
}
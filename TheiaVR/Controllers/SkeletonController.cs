using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers
{
    class SkeletonController
    {
        private static SkeletonController instance = null;

    
        private SkeletonController()
        {
            // Used to prevent public access
        }

        public static SkeletonController GetInstance()
        {
            if (instance == null)
            {
                instance = new SkeletonController();
            }
            return instance;
        }

        public void Start()
        {
            Messages.Log("SkeletonController started");
        }

    }
}

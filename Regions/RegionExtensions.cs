﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace PrismViewModelFirst.Regions
{
    public static class RegionExtensions
    {
        public static void RequestNavigateUsingSpecificContainer(this IRegion region, 
            Uri target, Action<NavigationResult> navigationCallback, 
            IUnityContainer containerToUse)
        {
            CustomRegionNavigationService moneycorpRegionNavigationService = region.NavigationService as CustomRegionNavigationService;
            if (moneycorpRegionNavigationService == null)
                throw new InvalidOperationException(
                    "RequestNavigate that takes a container may only be used with a CustomRegionNavigationService");

            ((CustomRegionNavigationService)region.NavigationService).RequestNavigate(
                target, navigationCallback, containerToUse);
        }
    }
}

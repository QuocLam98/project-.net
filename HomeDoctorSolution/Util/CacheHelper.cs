using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using HomeDoctorSolution.Models;

namespace HomeDoctorSolution.Util
{
    public interface ICacheHelper
    {
        //System config
        Dictionary<string, SystemConfig> GetSystemConfig();
        void SetSystemConfig(List<SystemConfig> systemConfigs);
        void SetSystemConfig(Dictionary<string, SystemConfig> systemConfigs);
        //Language Config
        Dictionary<string, LanguageConfig> GetLanguageConfig();
        void SetLanguageConfig(List<LanguageConfig> languageConfigs);
        void SetLanguageConfig(Dictionary<string, LanguageConfig> languageConfigs);
        List<Menu> GetMenuByRole();
        void SetMenuByRole(List<Menu> menuByRole);
        //Menu System Admin - groupId = 3 and menuTypeId = 1000005
        List<Menu> GetMenuSystemAdmin();
        void SetMenuSystemAdmin(List<Menu> menuAdmin);
        //Menu Aucton House Admin - groupId = 3 and menuTypeId = 1000005
        List<Menu> GetMenuAuctionHouseAdmin();
        void SetMenuAuctionHouseAdmin(List<Menu> menuAdmin);
        //Menu Auctioneer - groupId = 3 and menuTypeId = 1000005
        List<Menu> GetMenuAuctioneerAdmin();
        void SetMenuAuctioneerAdmin(List<Menu> menuAdmin);
        //Menu Auctioneer - groupId = 3 and menuTypeId = 1000005
        List<Menu> GetMenuUserSellerAdmin();
        void SetMenuUserSellerAdmin(List<Menu> menuUserSeller);
        //Menu Auctioneer - groupId = 3 and menuTypeId = 1000005
        List<Menu> GetMenuOrthersAuctioneer();
        void SetMenuOrthersAuctioneer(List<Menu> menuOrthersAuctioneer);

        List<Menu> GetMenuOrthersAuctioneerConfigAuctionHouse();
        void SetMenuOrthersAuctioneerConfigAuctionHouse(List<Menu> menuOrthersAuctioneerConfigAuctionHouse);

        //Menu Auction Assistant - groupId = 3 and menuTypeId = 1000005
        List<Menu> GetMenuAuctionAssistantAdmin();
        void SetMenuAuctionAssistantAdmin(List<Menu> menuAdmin);
        //List Right
        List<Right> GetRights();
        void SetRights(List<Right> rights);
        //List all Menu
        List<Menu> GetMenu();
        void SetMenu(List<Menu> menu);
        //List RoleRight của thằng chưa đăng nhập
        List<RoleRight> GetRoleRightsNotLogin();
        void SetRoleRightsNotLogin(List<RoleRight> roleRights);
        //List role rights
        List<RoleRight> GetRoleRights();
        void SetRoleRights(List<RoleRight> roleRightsTeenagerMod);
        List<RoleRight> GetRoleRightsTeenagerMod();
        void SetRoleRightsTeenagerMod(List<RoleRight> roleRightsAdminSchool);
        List<RoleRight> GetRoleRightsAdminSchool();
        void SetRoleRightsAdminSchool(List<RoleRight> roleRightsAdminSchool);
        //List Role menu
        List<RoleMenu> GetRoleMenu();
        void SetRoleMenu(List<RoleMenu> roleMenu);
    }

    public class CacheHelper : ICacheHelper
    {
        private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        //System config
        public Dictionary<string, SystemConfig> GetSystemConfig()
        {
            Dictionary<string, SystemConfig> obj = null;
            _cache.TryGetValue<string>("SystemConfig", out var json);

            if (json != null)
            {
                obj = JsonConvert.DeserializeObject<Dictionary<string, SystemConfig>>(json);
            }

            return obj;
        }

        public void SetSystemConfig(List<SystemConfig> systemConfigs)
        {
            SetSystemConfig(systemConfigs.ToDictionary(p => p.Code));
        }


        public void SetSystemConfig(Dictionary<string, SystemConfig> systemConfigs)
        {
            var json = JsonConvert.SerializeObject(systemConfigs);
            _cache.Set("SystemConfig", json);
        }

        //Language Config
        public Dictionary<string, LanguageConfig> GetLanguageConfig()
        {
            Dictionary<string, LanguageConfig> obj = null;
            _cache.TryGetValue<string>("LanguageConfig", out var json);

            if (json != null)
            {
                obj = JsonConvert.DeserializeObject<Dictionary<string, LanguageConfig>>(json);
            }

            return obj;
        }

        public void SetLanguageConfig(List<LanguageConfig> languageConfigs)
        {
            SetLanguageConfig(languageConfigs.ToDictionary(p => p.Code));
        }

        public void SetLanguageConfig(Dictionary<string, LanguageConfig> languageConfigs)
        {
            var json = JsonConvert.SerializeObject(languageConfigs);
            _cache.Set("LanguageConfig", json);
        }

        //Menu System Admin
        public List<Menu> GetMenuSystemAdmin()
        {
            _cache.TryGetValue<List<Menu>>("menuSystemAdmin", out var json);

            return json;
        }
        public void SetMenuSystemAdmin(List<Menu> menuAdmins)
        {
            _cache.Set("menuSystemAdmin", menuAdmins);
        }
        //Menu Auction House Admin
        public List<Menu> GetMenuAuctionHouseAdmin()
        {
            _cache.TryGetValue<List<Menu>>("menuAuctionHouseAdmin", out var json);

            return json;
        }
        public void SetMenuAuctionHouseAdmin(List<Menu> menuAdmins)
        {
            _cache.Set("menuAuctionHouseAdmin", menuAdmins);
        }
        //Menu Auctioneer Admin
        public List<Menu> GetMenuAuctioneerAdmin()
        {
            _cache.TryGetValue<List<Menu>>("menuAuctioneerAdmin", out var json);

            return json;
        }
        public void SetMenuAuctioneerAdmin(List<Menu> menuAdmins)
        {
            _cache.Set("menuAuctioneerAdmin", menuAdmins);
        }
        //Menu User Seller
        public List<Menu> GetMenuUserSellerAdmin()
        {
            _cache.TryGetValue<List<Menu>>("menuUserSellerAdmin", out var json);

            return json;
        }
        public void SetMenuUserSellerAdmin(List<Menu> menuUserSeller)
        {
            _cache.Set("menuUserSellerAdmin", menuUserSeller);
        }
        //Menu Orthers auctioneer admin
        public List<Menu> GetMenuOrthersAuctioneer()
        {
            _cache.TryGetValue<List<Menu>>("menuOrthersAuctioneer", out var json);

            return json;
        }
        public void SetMenuOrthersAuctioneer(List<Menu> menuOrthersAuctioneer)
        {
            _cache.Set("menuOrthersAuctioneer", menuOrthersAuctioneer);
        }
        public void SetMenuOrthersAuctioneerConfigAuctionHouse(List<Menu> menuOrthersAuctioneerConfigAuctionHouse)
        {
            _cache.Set("menuOrthersAuctioneerConfigAuctionHouse", menuOrthersAuctioneerConfigAuctionHouse);
        }
        public List<Menu> GetMenuOrthersAuctioneerConfigAuctionHouse()
        {
            _cache.TryGetValue<List<Menu>>("menuOrthersAuctioneerConfigAuctionHouse", out var json);

            return json;
        }
        //Menu Auction Assistant Admin
        public List<Menu> GetMenuAuctionAssistantAdmin()
        {
            _cache.TryGetValue<List<Menu>>("menuAuctionAssistantAdmin", out var json);

            return json;
        }
        public void SetMenuAuctionAssistantAdmin(List<Menu> menuAdmins)
        {
            _cache.Set("menuAuctionAssistantAdmin", menuAdmins);
        }
        //List Right
        public List<Right> GetRights()
        {
            _cache.TryGetValue<List<Right>>("Right", out var json);
            return json;
        }
        public void SetRights(List<Right> rights)
        {
            _cache.Set("Right", rights);
        }
        //list Menu
        public List<Menu> GetMenu()
        {
            _cache.TryGetValue<List<Menu>>("Menu", out var json);
            return json;
        }
        public void SetMenu(List<Menu> menu)
        {
            _cache.Set("Menu", menu);
        }
        //list RoleRegihts not login
        public List<RoleRight> GetRoleRightsNotLogin()
        {
            _cache.TryGetValue<List<RoleRight>>("RoleRightsNotLogin", out var json);
            return json;
        }
        public void SetRoleRightsNotLogin(List<RoleRight> roleRightsNotLogin)
        {
            _cache.Set("RoleRightsNotLogin", roleRightsNotLogin);
        }
        //Set roleright
        public List<RoleRight> GetRoleRights()
        {
            _cache.TryGetValue<List<RoleRight>>("ListRoleRights", out var json);
            return json;
        }
        public void SetRoleRights(List<RoleRight> roleRights)
        {
            _cache.Set("ListRoleRights", roleRights);
        }
        public List<RoleRight> GetRoleRightsTeenagerMod()
        {
            _cache.TryGetValue<List<RoleRight>>("ListRoleRightsTeenagerMod", out var json);
            return json;
        }
        public void SetRoleRightsTeenagerMod(List<RoleRight> roleRightsTeenagerMod)
        {
            _cache.Set("ListRoleRightsTeenagerMod", roleRightsTeenagerMod);
        }


        public List<RoleRight> GetRoleRightsAdminSchool()
        {
            _cache.TryGetValue<List<RoleRight>>("ListRoleRightsAdminSchool", out var json);
            return json;
        }
        public void SetRoleRightsAdminSchool(List<RoleRight> roleRightsAdminSchool)
        {
            _cache.Set("ListRoleRightsAdminSchool", roleRightsAdminSchool);
        }

        //Set roleMenu
        public List<RoleMenu> GetRoleMenu()
        {
            _cache.TryGetValue<List<RoleMenu>>("ListRoleMenu", out var json);
            return json;
        }
        public void SetRoleMenu(List<RoleMenu> roleMenu)
        {
            _cache.Set("ListRoleMenu", roleMenu);
        }
        //Menu System Admin
        public List<Menu> GetMenuByRole()
        {
            _cache.TryGetValue<List<Menu>>("menuByRole", out var json);

            return json;
        }
        public void SetMenuByRole(List<Menu> menuByRole)
        {
            _cache.Set("menuByRole", menuByRole);
        }
    }
}

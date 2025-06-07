using Il2Cpp;
using Il2CppNodeCanvas.Tasks.Actions;
using Il2CppProCore.Decals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Il2CppMono.Security.X509.X520;

namespace Toolbelts
{
    internal class TBFunctionalities
    {

        internal static string attachBeltText;
        private static GameObject attachBeltButton;
        internal static GearItem beltItem;
        internal static string beltName;

        internal static string detachBeltText;
        private static GameObject detachBeltButton;
        internal static GearItem pantsItem;
        internal static string pantsName;

        internal static string attachCramponsText;
        private static GameObject attachCramponsButton;
        internal static GearItem cramponItem;
        internal static string cramponName;

        internal static string detachCramponsText;
        private static GameObject detachCramponsButton;
        internal static GearItem bootsItem;
        internal static string bootsName;

        internal static string attachScabbardText;
        private static GameObject attachScabbardButton;
        internal static GearItem scabbardItem;
        internal static string scabbardName;

        internal static string detachScabbardText;
        private static GameObject detachScabbardButton;
        internal static GearItem bagItem;
        internal static string bagName;

        internal static void InitializeMTB(ItemDescriptionPage itemDescriptionPage)
        {
            attachBeltText = Localization.Get("GAMEPLAY_TB_AttachBeltLabel");
            detachBeltText = Localization.Get("GAMEPLAY_TB_DetachBeltLabel");
            attachCramponsText = Localization.Get("GAMEPLAY_TB_AttachCramponsLabel");
            detachCramponsText = Localization.Get("GAMEPLAY_TB_DetachCramponsLabel");
            attachScabbardText = Localization.Get("GAMEPLAY_TB_AttachScabbardLabel");
            detachScabbardText = Localization.Get("GAMEPLAY_TB_DetachScabbardLabel");

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            attachBeltButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            attachBeltButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(attachBeltButton).text = attachBeltText;

            detachBeltButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            detachBeltButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(detachBeltButton).text = detachBeltText;

            attachCramponsButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            attachCramponsButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(attachCramponsButton).text = attachCramponsText;

            detachCramponsButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            detachCramponsButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(detachCramponsButton).text = detachCramponsText;

            attachScabbardButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            attachScabbardButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(attachScabbardButton).text = attachScabbardText;

            detachScabbardButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            detachScabbardButton.transform.Translate(0, 0, 0);
            Utils.GetComponentInChildren<UILabel>(detachScabbardButton).text = detachScabbardText;


            AddAction(attachBeltButton, new System.Action(OnAttachBelt));
            AddAction(detachBeltButton, new System.Action(OnDetachBelt));

            AddAction(attachCramponsButton, new System.Action(OnAttachCrampons));
            AddAction(detachCramponsButton, new System.Action(OnDetachCrampons));

            AddAction(attachScabbardButton, new System.Action(OnAttachScabbard));
            AddAction(detachScabbardButton, new System.Action(OnDetachScabbard));

            SetAttachBeltActive(false);
            SetDetachBeltActive(false);

            SetAttachCramponsActive(false);
            SetDetachCramponsActive(false);

            SetAttachScabbardActive(false);
            SetDetachScabbardActive(false);

        }
        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }
        internal static void SetAttachBeltActive(bool active)
        {
            NGUITools.SetActive(attachBeltButton, active);
        }

        internal static void SetDetachBeltActive(bool active)
        {
            NGUITools.SetActive(detachBeltButton, active);
        }

        internal static void SetAttachCramponsActive(bool active)
        {
            NGUITools.SetActive(attachCramponsButton, active);
        }

        internal static void SetDetachCramponsActive(bool active)
        {
            NGUITools.SetActive(detachCramponsButton, active);
        }

        internal static void SetAttachScabbardActive(bool active)
        {
            NGUITools.SetActive(attachScabbardButton, active);
        }

        internal static void SetDetachScabbardActive(bool active)
        {
            NGUITools.SetActive(detachScabbardButton, active);
        }

        private static void OnAttachCrampons()
        {
            var thisGearItem = cramponItem;
            GearItem crampon = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_Crampons");
            GearItem cramponimprov = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_ImprovisedCrampons");

            if (thisGearItem == null) return;
            if (crampon == null && cramponimprov == null)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TB_NoCrampons"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (thisGearItem.name is "GEAR_WorkBoots" or "GEAR_BasicBoots" or "GEAR_CombatBoots" or "GEAR_InsulatedBoots" or "GEAR_BasicShoes" or "GEAR_SkiBoots" or "GEAR_LeatherShoes" or "GEAR_DeerSkinBoots" or "GEAR_MuklukBoots" or "GEAR_MinersBoots")
            {
                cramponName = thisGearItem.name;
                if (crampon)
                {
                    GameAudioManager.PlayGuiConfirm();
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_AttachingProgressBar"), 1f, 0f, 0f,
                                    "PLAY_CRACCESSORIES_LEATHERBELT_EQUIP", null, false, true, new System.Action<bool, bool, float>(OnAttachCramponsFinished));
                    GameManager.GetInventoryComponent().RemoveGear(crampon.gameObject);
                }
                else if (cramponimprov)
                {
                    GameAudioManager.PlayGuiConfirm();
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_AttachingProgressBar"), 1f, 0f, 0f,
                                    "PLAY_CRACCESSORIES_LEATHERBELT_EQUIP", null, false, true, new System.Action<bool, bool, float>(OnAttachCramponsUFinished));
                    GameManager.GetInventoryComponent().RemoveGear(cramponimprov.gameObject);
                }
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TB_NoCrampons"));
                GameAudioManager.PlayGUIError();
            }

        }
        private static void OnAttachCramponsFinished(bool success, bool playerCancel, float progress)
        {
            switch (cramponName)
            {
                case "GEAR_WorkBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.workBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_CombatBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.combatBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_DeerSkinBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.deerBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_InsulatedBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.insulatedBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_LeatherShoes":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.dressingBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_MuklukBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.muklukBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_BasicShoes":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.runningBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_SkiBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.skiBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_BasicBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.trailBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_MinersBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.chemicalBootsCramp, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
            }
            GameManager.GetInventoryComponent().RemoveGear(cramponItem.gameObject);

        }

        private static void OnAttachCramponsUFinished(bool success, bool playerCancel, float progress)
        {
            GearItem pants;

            switch (cramponName)
            {
                case "GEAR_WorkBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.workBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_CombatBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.combatBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_DeerSkinBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.deerBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_InsulatedBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.insulatedBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_LeatherShoes":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.dressingBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_MuklukBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.muklukBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_BasicShoes":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.runningBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_SkiBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.skiBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_BasicBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.trailBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
                case "GEAR_MinersBoots":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.chemicalBootsImprov, 1).m_CurrentHP = cramponItem.m_CurrentHP;
                    break;
            }
            GameManager.GetInventoryComponent().RemoveGear(cramponItem.gameObject);
        }

        private static void OnDetachCrampons()
        {
            var thisGearItem = bootsItem;
            var crampon = ToolbeltsUtils.crampons;
            var cramponI = ToolbeltsUtils.cramponsimprov;

            if (thisGearItem == null) return;

            if (thisGearItem.name is "GEAR_WorkNCrampons" or "GEAR_CombatNCrampons" or "GEAR_DeerskinNCrampons" or "GEAR_InsulatedNCrampons" or "GEAR_DressingNCrampons" or "GEAR_MuklukNCrampons" or "GEAR_RunningNCrampons" or "GEAR_SkiNCrampons" or "GEAR_TrailNCrampons" or "GEAR_ChemicalNCrampons")
            {
                bootsName = thisGearItem.name;
                    GameAudioManager.PlayGuiConfirm();
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_DetachingProgressBar"), 1f, 0f, 0f,
                                    "PLAY_CRACCESSORIES_LEATHERBELT_UNQUIP", null, false, true, new System.Action<bool, bool, float>(OnDetachCramponsFinished));
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.crampons, 1);
            }
            else if (thisGearItem.name is "GEAR_WorkICrampons" or "GEAR_CombatICrampons" or "GEAR_DeerskinICrampons" or "GEAR_InsulatedICrampons" or "GEAR_DressingICrampons" or "GEAR_MuklukICrampons" or "GEAR_RunningICrampons" or "GEAR_SkiICrampons" or "GEAR_TrailICrampons" or "GEAR_ChemicalICrampons")
            {
                bootsName = thisGearItem.name;
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_DetachingProgressBar"), 1f, 0f, 0f,
                    "PLAY_CRACCESSORIES_LEATHERBELT_UNQUIP", null, false, true, new System.Action<bool, bool, float>(OnDetachCramponsFinished));
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.cramponsimprov, 1);
            }

        }
        private static void OnDetachCramponsFinished(bool success, bool playerCancel, float progress)
        {

            switch (bootsName)
            {
                case "GEAR_WorkNCrampons":
                case "GEAR_WorkICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.workBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_CombatNCrampons":
                case "GEAR_CombatICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.combatBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_DeerskinNCrampons":
                case "GEAR_DeerskinICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.deerBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_InsulatedNCrampons":
                case "GEAR_InsulatedICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.insulatedBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_DressingNCrampons":
                case "GEAR_DressingICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.dressingBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_MuklukNCrampons":
                case "GEAR_MuklukICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.muklukBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_RunningNCrampons":
                case "GEAR_RunningICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.runningBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_SkiNCrampons":
                case "GEAR_SkiICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.skiBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_TrailNCrampons":
                case "GEAR_TrailICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.trailBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
                case "GEAR_ChemicalNCrampons":
                case "GEAR_ChemicalICrampons":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.chemicalBoots, 1).m_CurrentHP = bootsItem.m_CurrentHP;
                    break;
            }

            GameManager.GetInventoryComponent().RemoveGear(bootsItem.gameObject);

        }
        private static void OnAttachBelt()
        {
            var thisGearItem = beltItem;
            GearItem beltU = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_Toolbelt");

            if (thisGearItem == null) return;
            if (beltU == null)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TB_NoBelt"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (thisGearItem.name is "GEAR_Jeans" or "GEAR_CargoPants" or "GEAR_CombatPants" or "GEAR_DeerSkinPants" or "GEAR_InsulatedPants" or "GEAR_MinersPants" or "GEAR_WorkPants")
            {
                beltName = thisGearItem.name;
                if (beltU)
                {
                    GameAudioManager.PlayGuiConfirm();
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_AttachingProgressBar"), 1f, 0f, 0f,
                                    "PLAY_CRACCESSORIES_LEATHERBELT_EQUIP", null, false, true, new System.Action<bool, bool, float>(OnAttachBeltUFinished));
                    GameManager.GetInventoryComponent().RemoveGear(beltU.gameObject);
                }
                               
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TB_NoBelt"));
                GameAudioManager.PlayGUIError();
            }

        }
        private static void OnAttachBeltUFinished(bool success, bool playerCancel, float progress)
        {
            switch (beltName)
            {
                case "GEAR_Jeans":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.jeansbelt, 1).m_CurrentHP = beltItem.m_CurrentHP;
                    break;
                case "GEAR_CargoPants":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.cargobelt, 1).m_CurrentHP = beltItem.m_CurrentHP;
                    break;
                case "GEAR_CombatPants":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.combatbelt, 1).m_CurrentHP = beltItem.m_CurrentHP;
                    break;
                case "GEAR_DeerSkinPants":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.deerskinbelt, 1).m_CurrentHP = beltItem.m_CurrentHP;
                    break;
                case "GEAR_InsulatedPants":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.insulatedbelt, 1).m_CurrentHP = beltItem.m_CurrentHP;
                    break;
                case "GEAR_MinersPants":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.minerbelt, 1).m_CurrentHP = beltItem.m_CurrentHP;
                    break;
                case "GEAR_WorkPants":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.workbelt, 1).m_CurrentHP = beltItem.m_CurrentHP;
                    break;
            }
            GameManager.GetInventoryComponent().RemoveGear(beltItem.gameObject);

        }

        private static void OnDetachBelt()
        {
            var thisGearItem = pantsItem;

            if (thisGearItem == null) return;
           
            if (thisGearItem.name is "GEAR_JeansToolbelt" or "GEAR_CargoToolbelt" or "GEAR_MinerToolbelt" or "GEAR_CombatToolbelt" or "GEAR_DeerskinToolbelt" or "GEAR_InsulatedToolbelt" or "GEAR_WorkToolbelt")
            {
                pantsName = thisGearItem.name;
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_DetachingProgressBar"), 1f, 0f, 0f,
                    "PLAY_CRACCESSORIES_LEATHERBELT_UNQUIP", null, false, true, new System.Action<bool, bool, float>(OnDetachBeltFinished));
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.belt, 1);
            }


        }
        private static void OnDetachBeltFinished(bool success, bool playerCancel, float progress)
        {

            switch (pantsName)
            {
                case "GEAR_JeansToolbelt":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.jeans, 1).m_CurrentHP = pantsItem.m_CurrentHP;
                    break;
                case "GEAR_CargoToolbelt":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.cargo, 1).m_CurrentHP = pantsItem.m_CurrentHP;
                    break;
                case "GEAR_MinerToolbelt":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.miner, 1).m_CurrentHP = pantsItem.m_CurrentHP;
                    break;
                case "GEAR_CombatToolbelt":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.combat, 1).m_CurrentHP = pantsItem.m_CurrentHP;
                    break;
                case "GEAR_DeerskinToolbelt":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.deerskin, 1).m_CurrentHP = pantsItem.m_CurrentHP;
                    break;
                case "GEAR_InsulatedToolbelt":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.insulated, 1).m_CurrentHP = pantsItem.m_CurrentHP;
                    break;
                case "GEAR_WorkToolbelt":
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.work, 1).m_CurrentHP = pantsItem.m_CurrentHP;
                    break;
            }
            GameManager.GetInventoryComponent().RemoveGear(pantsItem.gameObject);
        }

        private static void OnAttachScabbard()
        {
            var thisGearItem = scabbardItem;
            GearItem scab = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_RifleScabbardA");

            if (thisGearItem == null) return;
            if (scab == null)
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TB_NoScabbard"));
                GameAudioManager.PlayGUIError();
                return;
            }
            if (thisGearItem.name == "GEAR_MooseHideBag")
            {
                beltName = thisGearItem.name;
                if (scab)
                {
                    GameAudioManager.PlayGuiConfirm();
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_AttachingProgressBar"), 1f, 0f, 0f,
                                    "PLAY_CRACCESSORIES_LEATHERBELT_EQUIP", null, false, true, new System.Action<bool, bool, float>(OnAttachScabbardFinished));
                    GameManager.GetInventoryComponent().RemoveGear(scab.gameObject);
                }

            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_TB_NoScabbard"));
                GameAudioManager.PlayGUIError();
            }

        }
        private static void OnAttachScabbardFinished(bool success, bool playerCancel, float progress)
        {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.bagscabbard, 1).m_CurrentHP = scabbardItem.m_CurrentHP;

                GameManager.GetInventoryComponent().RemoveGear(scabbardItem.gameObject);
        }

        private static void OnDetachScabbard()
        {
            var thisGearItem = bagItem;

            if (thisGearItem == null) return;

            if (thisGearItem.name == "GEAR_MooseBagPlusScabbard")
            {
                bagName = thisGearItem.name;
                GameAudioManager.PlayGuiConfirm();
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_TB_DetachingProgressBar"), 1f, 0f, 0f,
                    "PLAY_CRACCESSORIES_LEATHERBELT_UNQUIP", null, false, true, new System.Action<bool, bool, float>(OnDetachScabbardFinished));
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.scabbard, 1);
            }


        }
        private static void OnDetachScabbardFinished(bool success, bool playerCancel, float progress)
        {
                GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(ToolbeltsUtils.bag, 1).m_CurrentHP = bagItem.m_CurrentHP;

                GameManager.GetInventoryComponent().RemoveGear(bagItem.gameObject);
        }

        }
}

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBot.Api;
using LeagueBot.ApiHelpers;
using LeagueBot.IO;

namespace LeagueBot.Game.Misc
{
    public class Shop : ApiMember<GameApi>
    {
        public bool Opened
        {
            get;
            set;
        }
        public List<Item> ItemsToBuy = new List<Item>();
        public Shop(GameApi api) : base(api)
        {
            this.Opened = false;
        }
        public void toogle()
        {
            InputHelper.PressKey("P");
            BotHelper.InputIdle();
            Opened = !Opened;
        }
        public void setItemBuild(List<Item> items)
        {
            if (ItemsToBuy != null)
                ItemsToBuy.Clear();

                foreach(Item _item in items)
                {
                    ItemsToBuy.Add(_item);

                    Logger.Write($"Added {_item.name} on items list");
                }
        }

        public int getPlayerGold()
        {
            return TextHelper.GetTextFromImage(767, 828, 118, 34);
        }

        public void tryBuyItem()
        {
            if (ItemsToBuy != null)
            {
                foreach (Item _item in ItemsToBuy)
                {
                    BotHelper.Wait(1000);
                    if (_item.cost <= getPlayerGold())
                    {
                        if (_item.got == false)
                        {
                            Logger.Write($"Character bought {_item.name}.");
                            InputHelper.RightClick(_item.point.X, _item.point.Y, 200);
                            _item.got = true;
                            
                            BotHelper.Wait(500);
                            Logger.Write($"{getPlayerGold().ToString()} gold remaining.");
                            tryBuyItem();
                            BotHelper.Wait(500);
                        }
                    }
                }
            }
        }
        public void buyItem(int indice)
        {
            //INITIAL BUY
            Point coords = new Point(0, 0);

            switch (indice)
            {
                case 1:
                    coords = new Point(577, 337);
                    break;
                case 2:
                    coords = new Point(782, 336);
                    break;
                case 3:
                    coords = new Point(595, 557);
                    break;
                case 4:
                    coords = new Point(600, 665);
                    break;
                case 5:
                    coords = new Point(760, 540);
                    break;
                default:
                    Logger.Write("Unknown item indice " + indice + ". Skipping", MessageState.WARNING);
                    return;
            }
            InputHelper.RightClick(coords.X, coords.Y);

            BotHelper.InputIdle();
        }
    }
}

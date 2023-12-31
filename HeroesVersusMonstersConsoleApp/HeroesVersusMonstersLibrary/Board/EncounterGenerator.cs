﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class EncounterGenerator
    {
		private Dictionary<int, Encounter> _encounterList = new Dictionary<int, Encounter>();

		public Dictionary<int, Encounter> EncounterList
		{
			get { return _encounterList; }
			private set { _encounterList = value; }
		}



		private Entity _hero;

		public Entity Hero
		{
			get { return _hero; }
			private set { _hero = value; }
		}

		private Engine _engine;

		public Engine Engine
		{
			get { return _engine; }
			set { _engine = value; }
		}


		public EncounterGenerator(Entity hero, Engine engine)
        {
            this._hero = hero;
			this._engine = engine;
        }


		//public void PresentEncounter()
		//{
		//	bool NotChosen = true;
		//	while (NotChosen)
		//	{

		//	}
		//}

		//public void GenerateEncounter()
		//{
		//	Random rnd = new Random();
		//	bool notFree = true;
		//	int encounterX = -1;
		//	int encounterY = -1;
		//	while (notFree)
		//	{
		//		encounterX = rnd.Next(160);
		//		encounterY = rnd.Next(16);
		//		if (encounterX != this._engine.HeroPosX && encounterY != this._engine.HeroPosY)
		//		{
		//			if (this._engine.ActiveMap.Map[encounterY][encounterX].Type == 0)
		//			{
		//				notFree = false;
		//			}
		//		}
		//	}

		//	Encounter newEncounter = new Encounter(this, 10, encounterX, encounterY);
		//	encounterX = 1;
		//	notFree = true;
		//	while (notFree)
		//	{
		//		encounterX = rnd.Next(-2000, -1000);
		//		notFree = false;
		//		foreach (KeyValuePair<int, Encounter> entry in this._encounterList)
		//		{
		//			if (entry.Key == encounterX)
		//			{
		//				notFree = true;
		//			}
		//		} 
		//	}
		//	this._encounterList.Add(encounterX, newEncounter);

		//	foreach(KeyValuePair<int, Encounter> entry in this._encounterList)
		//	{
		//		this._engine.ActiveMap.Map[entry.Value.PosY][entry.Value.PosX].ChangeType(entry.Key);
		//	}
		//}



        public void GenerateEncounterRoomOne(int startx, int starty, int horizontalsize, int verticalsize)
        {
            Random rnd = new Random();
            bool notFree = true;
            int encounterX = -1;
			int encounterY = -1;
			int encounterCounter = 0;
			foreach (KeyValuePair<int, Encounter> encounter in this._encounterList)
			{
				if (encounter.Value.PosX < startx + horizontalsize && encounter.Value.PosX > startx && encounter.Value.PosY > starty && encounter.Value.PosY < starty + verticalsize)
				{
					encounterCounter++;
				}
			}
			while (encounterCounter < 5)
			{
                while (notFree)
                {
                    encounterX = rnd.Next(startx, startx + horizontalsize);
                    encounterY = rnd.Next(starty, starty + verticalsize);
                    if (encounterX != this._engine.HeroPosX && encounterY != this._engine.HeroPosY)
                    {
                        if (this._engine.ActiveMap.Map[encounterY][encounterX].Type == 0)
                        {
                            notFree = false;
                        }
                    }
                }

                Encounter newEncounter = new Encounter(this, 10, encounterX, encounterY);
                int encounterID = 1;
                notFree = true;
                while (notFree)
                {
                    encounterID = rnd.Next(-2000, -1000);
                    notFree = false;
                    foreach (KeyValuePair<int, Encounter> entry in this._encounterList)
                    {
                        if (entry.Key == encounterID)
                        {
                            notFree = true;
                        }
                    }
                }
                this._encounterList.Add(encounterID, newEncounter);


                foreach (KeyValuePair<int, Encounter> entry in this._encounterList)
                {
                    this._engine.ActiveMap.Map[entry.Value.PosY][entry.Value.PosX].ChangeType(entry.Key);
                }
                encounterCounter++;
                notFree = true;
            }
        }

    }
}

//#region Test monsters creations
//Monster monster3 = new Monster(1);
//Monster monster4 = new Monster(0);


//List<Entity> testEntities = new List<Entity>();

//testEntities.Add(hero1);
//testEntities.Add(monster3);
//testEntities.Add(monster4);

//#endregion

//Board testBoard = new Board(testEntities);

//foreach (Entity entity in testEntities)
//{
//    entity.OnHit += testBoard.OnHitHandler;
//}

//testBoard.Encounter();
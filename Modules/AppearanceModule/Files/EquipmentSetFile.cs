﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace ConceptMatrix.AppearanceModule.Files
{
	using System;
	using ConceptMatrix.AppearanceModule.ViewModels;
	using ConceptMatrix.AppearanceModule.Views;

	[Serializable]
	public class EquipmentSetFile : FileBase
	{
		public static readonly FileType FileType = new FileType("cm3eq", "Equipment Set", typeof(EquipmentSetFile));

		public Weapon MainHand { get; set; } = new Weapon();
		public Vector MainHandScale { get; set; } = Vector.One;
		public Color MainHandColor { get; set; } = Color.White;
		public Weapon OffHand { get; set; } = new Weapon();
		public Vector OffHandScale { get; set; } = Vector.One;
		public Color OffHandColor { get; set; } = Color.White;

		public Item Head { get; set; } = new Item();
		public Item Body { get; set; } = new Item();
		public Item Hands { get; set; } = new Item();
		public Item Legs { get; set; } = new Item();
		public Item Feet { get; set; } = new Item();
		public Item Ears { get; set; } = new Item();
		public Item Neck { get; set; } = new Item();
		public Item Wrists { get; set; } = new Item();
		public Item LeftRing { get; set; } = new Item();
		public Item RightRing { get; set; } = new Item();

		public override FileType GetFileType()
		{
			return FileType;
		}

		public void Read(EquipmentEditor vm)
		{
			this.MainHand.Read(vm.MainHand);
			this.MainHandScale = vm.MainHand.Scale;
			this.MainHandColor = vm.MainHand.Color;

			this.OffHand.Read(vm.OffHand);
			this.OffHandScale = vm.OffHand.Scale;
			this.OffHandColor = vm.OffHand.Color;

			this.Head.Read(vm.Head);
			this.Body.Read(vm.Body);
			this.Hands.Read(vm.Hands);
			this.Legs.Read(vm.Legs);
			this.Feet.Read(vm.Feet);
			this.Ears.Read(vm.Ears);
			this.Neck.Read(vm.Neck);
			this.Wrists.Read(vm.Wrists);
			this.LeftRing.Read(vm.LeftRing);
			this.RightRing.Read(vm.RightRing);
		}

		public void WritePreRefresh(EquipmentEditor vm)
		{
			if (this.MainHand.ModelSet != 0)
				this.MainHand.Write(vm.MainHand);

			this.OffHand.Write(vm.OffHand);
			this.Head.Write(vm.Head);
			this.Body.Write(vm.Body);
			this.Hands.Write(vm.Hands);
			this.Legs.Write(vm.Legs);
			this.Feet.Write(vm.Feet);
			this.Ears.Write(vm.Ears);
			this.Neck.Write(vm.Neck);
			this.Wrists.Write(vm.Wrists);
			this.LeftRing.Write(vm.LeftRing);
			this.RightRing.Write(vm.RightRing);
		}

		public void WritePostRefresh(EquipmentEditor vm)
		{
			// These values are reset by the game when refreshing the actor
			// (required to change equipment) to we must write them again
			// once the refresh is complete.
			vm.MainHand.Scale = this.MainHandScale;
			vm.MainHand.Color = this.MainHandColor;
			vm.OffHand.Scale = this.OffHandScale;
			vm.OffHand.Color = this.OffHandColor;
		}

		[Serializable]
		public class Weapon : Item
		{
			public Color Color { get; set; }
			public Vector Scale { get; set; }
			public ushort ModelSet { get; set; }

			public override void Write(EquipmentBaseViewModel to)
			{
				to.Color = this.Color;
				to.Scale = this.Scale;
				to.ModelSet = this.ModelSet;

				base.Write(to);
			}

			public override void Read(EquipmentBaseViewModel from)
			{
				this.Color = from.Color;
				this.Scale = from.Scale;
				this.ModelSet = from.ModelSet;

				base.Read(from);
			}
		}

		[Serializable]
		public class Item
		{
			public ushort ModelBase { get; set; }
			public ushort ModelVariant { get; set; }
			public byte DyeId { get; set; }

			public virtual void Write(EquipmentBaseViewModel to)
			{
				to.ModelBase = this.ModelBase;
				to.ModelVariant = this.ModelVariant;
				to.DyeId = this.DyeId;
			}

			public virtual void Read(EquipmentBaseViewModel from)
			{
				this.ModelBase = from.ModelBase;
				this.ModelVariant = from.ModelVariant;
				this.DyeId = from.DyeId;
			}
		}
	}
}

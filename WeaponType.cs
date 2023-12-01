using System.ComponentModel.DataAnnotations;

public enum WeaponType {
  Axe,
  Dagger,
  Mace,
  Spear,
  Sword,
  [Display(Name = "Ceremonial Knife")]
  CeremonialKnife,
  [Display(Name = "Fist Weapon")]
  FistWeapon,
  Flail,
  [Display(Name = "Mighty Weapon")]
  MightyWeapon,
  Scythe,
  Polearm,
  Staff,
  Daibo,
  Bow,
  Crossbow,
  [Display(Name = "Hand Crossbow")]
  HandCrossbow,
  Wand,
}
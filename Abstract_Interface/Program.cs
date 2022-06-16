// See https://aka.ms/new-console-template for more information

using Abstract_Interface;

SchoolBell sb = new();
ChruchBell cb = new();

sb.Ring();
cb.Ring();

sb.IncreaseVolume();
cb.DecreaseVolume();

Console.WriteLine(sb.ToString());

ISchoolBell isb = new();
IChurchBell icb = new();

isb.Ring();
icb.Ring();

isb.IncreaseVolume();
icb.DecreaseVolume();
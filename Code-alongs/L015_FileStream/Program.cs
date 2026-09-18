

using (FileStream stream = File.OpenWrite("myFile.bin"))
{
	for (byte i = 0; i < 255; i++)
	{
		stream.WriteByte(i);
	}
}


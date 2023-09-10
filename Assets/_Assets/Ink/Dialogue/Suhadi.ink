INCLUDE DialogueData.ink

{
- suhadi == false: -> once
}

#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Hmm... mungkin aku juga akan bergabung.
-> END

=== once ===
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Kau dengar itu Karman? mereka membentuk pasukan polisi baru!
Namanya kalau tidak salah, Badan Keamanan Rakjat, katanya?
* [Iya, benar]
Setelah PETA dibubarkan minggu lalu kita akhirnya dapat pulang ke kota Surabaya, namun sepertinya hal itu tidak berlangsung selamanya ya? Hahaha.
#speaker:Karman #portrait:null #audio:alphabet
Tidak, menurutku hal itu adalah langkah yang bagus. Kita membutuhkan suatu aparat keamanan untuk menjaga ketentraman ini secepatnya.
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Hmmm... begitukah?
Bagaimana menurutmu, apakah bung akan bergabung?
#speaker:Karman #portrait:null #audio:alphabet
Yah bagimanapun, kita sebagai mantan pasukan PETA mempunyai pengalaman. Tidak ada salahnya untuk mencoba bergabung.
~ suhadi = true
-> END

=== selebaran ===
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Ada apa bung? kenapa kembali lagi?
#speaker:Karman #portrait:null #audio:alphabet
Bung, baru saja ada pesawat Belanda mengirimkan selebaran.
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Wah! Aku akan keluar sebentar lagi untuk melihat-lihat.
Aku mau ganti baju terlebih dahulu.
#speaker:Karman #portrait:null #audio:alphabet
Baiklah, Kalau begitu Aku akan pergi ke kantor BKR untuk melaporkan hal ini.
-> END
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Selamat siang, ada yang bisa saya bantu bung?
-> END

=== GiveSelebaran ===
#speaker:Karman #portrait:null #audio:alphabet
Selamat siang bung, Baru saja ada pesawat Belanda membagikan selebaran di langit Surabaya.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Ya. Saya sudah mendengar kabar tersebut.
Sepertinya pasukan sekutu akan datang ke Surabaya.
Tapi Saya belum membacanya isi selebarannya.
Apakah bung mempunyai kertas selebarannya?
#speaker:Karman #portrait:null #audio:alphabet
Ya, saya membawanya.
#speaker:null #portrait:null #audio:null
Kau memberikan selebaran kepada Bung Jasin.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
...
Hmm, menjemput para interniran?
Kita harus mengadakan rapat mengenai hal ini.
Hei! panggil para atasan BKR untuk berkumpul sore ini.
Oh, panggil juga Bung Tomo.
#speaker:Prajurit Gelisah #portrait:null #audio:pak_karto
Baik pak!
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
...
Bung, siapa namamu?
#speaker:Karman #portrait:null #audio:alphabet
Karman.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Baiklah, bung Karman. Sepertinya bung kesini tidak hanya untuk memberikan selebaran ini kan?
#speaker:Karman #portrait:null #audio:alphabet
Benar. Saya adalah mantan anggota pasukan PETA di Blitar.
Saya kesini untuk bergabung menjadi anggota BKR.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Hahaha! Baiklah. Dengan semangat itu, bagaimana mungkin saya menolak proposalmu.
Dengan ini, karena bung juga yang telah memberitahuku mengenai selebaran itu.
Secara resmi bung telah menjadi anggota BKR.
Saya harap nanti bung siap untuk mengikuti rapat mengenai hal ini.
#speaker:Karman #portrait:null #audio:alphabet
Ya! Terima kasih atas pertimbangannya.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Bicaralah padaku apabila bung telah siap untuk mengikuti rapat ini.
-> END

=== StartRapat ===
EXTERNAL ChangeScene(sceneName)
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Bagaimana? Apakah bung telah siap untuk mengikuti rapat ini?
* [Saya siap]
    Baiklah. Kalau begitu rapat ini akan dimulai.
    ~ ChangeScene("ARC2_PERSIAPAN")
    -> END
* [Belum siap]
    Ya, bicaralah padaku apabila bung telah siap untuk mengikuti rapat ini.
    -> END
-> END

=== PesawatSelebaran ===
#speaker:Karman #portrait:null #audio:alphabet
Selamat siang bung, Baru saja ada pesawat Belanda membagikan selebaran di langit Surabaya.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Ya. Saya sudah mendengar kabar tersebut.
Sepertinya pasukan sekutu akan datang ke Surabaya.
Tapi Saya belum membacanya isi selebarannya.
Apakah bung mempunyai kertas selebarannya?
#speaker:Karman #portrait:null #audio:alphabet
Oh, maaf Saya tidak membawanya.
Akan saya bawakan untuk bung.
-> END
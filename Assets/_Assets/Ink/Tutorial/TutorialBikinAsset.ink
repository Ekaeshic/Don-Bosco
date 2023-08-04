// Baca tutorial di yt: https://www.youtube.com/watch?v=KSRpcftVyKg
//
// Script untuk read dialog ink ini udah kumodif untuk fleksibilitas akses
// Tag seperti Audio, Speaker dan Portrait dapat dihilangkan
// Dengan mengisinya pake value:null <-----
//
// Jika setiap line tidak disebutkan value tag, tampilan default akan
// dimunculkan oleh DialogueManager (see line +-165)
//
// Ini adalah Contoh penggunaan dialog tanpa choice sederhana.
// Tutorial ini hanya menjelaskan perubahan yang mempermudah pembuatan dialog,
// Sisanya dapat melihat guide untuk format cara pembuatan dialog pake ink.
//
// Contoh ini menggunakan audio voiceover,
// Untuk membuat dialog tanpa audio, gunakan: #audio:null
INCLUDE tutorialData.ink

//VAR MC_name = "Arul" //Deklarasi variable, tapi tidak perlu karena sudah inherit dari variable global.

// Menghilangkan speaker dan portrait akan memberikan kesan narator
#speaker:null 
#portrait:null audio:alphabet
Pria itu terlihat sangat asyik bermain dengan kucing yang ada di sebelahnya

#speaker:{MC_name}
#portrait:null #audio:alphabet
Halo mas, lagi ngapain? hehe..
// Jika orang yang sama masih berbicara, bisa langsung tanpa tulis tag
Oh, perkenalin mas, namaku <color=\#FFD258>{MC_name}</color>.
...
Kucingnya lucu ya mas.

#speaker:Adit 
#portrait:null #audio:adit
Lucu banget mas!

-> END
// WAJIB ADA END

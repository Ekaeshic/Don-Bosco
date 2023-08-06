INCLUDE tutorialData.ink

{
- itemGiven == true: -> complete
- hasItem == false: 
    #speaker:Yudi
    #portrait:null #audio:celeste_low
    Mas, tolong dong.
    Daritadi saya cari selembar kertas.
    Kalau nemu bisa diberikan ke saya? makasih mas.
- hasItem == true:
    #speaker:{MC_name} 
    #portrait:null #audio:alphabet
    Ini pak, kertasnya.
    #speaker:Yudi
    #portrait:null #audio:celeste_low
    Wah, makasih mas!
    ~ itemGiven = true
}
- -> END

=== complete ===
    #speaker:Yudi
    #portrait:null #audio:celeste_low
    Makasih ya mas, sekarang saya bisa ajukan banding UKT kuliah saya, hehe...
    -> END

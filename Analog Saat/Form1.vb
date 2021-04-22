Public Class Form1
    Private OrtaX, OrtaY, ResimX, ResimY, CurrentXsn, CurrentYsn, CurrentXdk, CurrentYdk, CurrentXsa, CurrentYsa As Single
    Private Se, Sn, Dk, Sa, Kadran As Short
    Private Function KoordinatX(ByVal Derece As Single, ByVal IbreUzunluk As Short, ByVal KisalmaMiktari As Short, ByVal BaslangicX As Short) As Single
        Dim Radyan As Single
        Radyan = (22 / 7 * Derece) / 180    'derece cinsinden ifade radyana çevriliyor
        KoordinatX = BaslangicX + System.Math.Cos(Radyan) * (IbreUzunluk - KisalmaMiktari)  'belirtilen değerlere göre ikinci x ekseni değeri veriliyor
    End Function
    Private Function KoordinatY(ByVal Derece As Single, ByVal IbreUzunluk As Short, ByVal KisalmaMiktari As Short, ByVal BaslangicY As Short) As Single
        Dim Radyan As Single
        Radyan = (22 / 7 * Derece) / 180    'derece cinsinden ifade radyana çevriliyor
        KoordinatY = BaslangicY + System.Math.Sin(Radyan) * (IbreUzunluk - KisalmaMiktari)  'belirtilen değerlere göre ikinci y ekseni değeri veriliyor
    End Function
    Private Sub HazirlikYap()
        Dim OrtakX, OrtakY, SaatX1, SaatY1, DakikaX1, DakikaY1 As Single
        Dim Kad As Short
        For Kad = 0 To 360 Step 6
            OrtakX = KoordinatX(Kad, Kadran, 0, OrtaX)
            OrtakY = KoordinatY(Kad, Kadran, 0, OrtaY)

            If Kad Mod 30 = 0 Then
                SaatX1 = KoordinatX(Kad, Kadran, 20, OrtaX)
                SaatY1 = KoordinatY(Kad, Kadran, 20, OrtaY)
                PictureBox1.CreateGraphics.DrawLine(System.Drawing.Pens.Red, SaatX1, SaatY1, OrtakX, OrtakY)
            Else
                DakikaX1 = KoordinatX(Kad, Kadran, 10, OrtaX)
                DakikaY1 = KoordinatY(Kad, Kadran, 10, OrtaY)
                PictureBox1.CreateGraphics.DrawLine(System.Drawing.Pens.Blue, DakikaX1, DakikaY1, OrtakX, OrtakY)
            End If
        Next
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ResimX = PictureBox1.Size.Width 'resim alanının,
        ResimY = PictureBox1.Size.Height 'boyunları alınıyor

        OrtaX = ResimX / 2  'resmin yatay eksendeki orta noktası
        OrtaY = ResimY / 2 'resmin dikey eksendeki orta noktası

        Sa = (270 + (Hour(Date.Now) Mod 12) * 30) Mod 360 'ilk etapdaki saat açı değeri değeri
        Dk = (270 + Minute(Date.Now) * 6) Mod 360   'ilk etaptaki dakika açı değeri değeri
        Sn = 276 + Second(Date.Now) * 6 'ilk etapdaki saniye açı değeri
        Se = 276 + Second(Date.Now) * 6 'ilk etapdaki saniye açı değeri

        Kadran = Fix((OrtaX + OrtaY) / 2)   'saat ibresinin uzunluğu

        CurrentXsa = KoordinatX(Sa, Kadran, 80, OrtaX)  'ilk etapta sistemin saati okunduktan sonra
        CurrentYsa = KoordinatY(Sa, Kadran, 80, OrtaY)  'ibreler sistemin saatine göre konumlandırılıyor

        CurrentXdk = KoordinatX(Dk, Kadran, 50, OrtaX)
        CurrentYdk = KoordinatY(Dk, Kadran, 50, OrtaY)

        CurrentXsn = KoordinatX(Sn, Kadran, 30, OrtaX)
        CurrentYsn = KoordinatY(Sn, Kadran, 30, OrtaY)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PictureBox1.CreateGraphics.Clear(System.Drawing.Color.Silver)
        PictureBox1.CreateGraphics.DrawEllipse(System.Drawing.Pens.Silver, 0, 0, ResimX - 1, ResimY - 1)
        HazirlikYap()
        PictureBox1.CreateGraphics.DrawLine(System.Drawing.Pens.LightYellow, OrtaX, OrtaY, CurrentXsa, CurrentYsa)
        PictureBox1.CreateGraphics.DrawLine(System.Drawing.Pens.Cyan, OrtaX, OrtaY, CurrentXdk, CurrentYdk)
        PictureBox1.CreateGraphics.DrawLine(System.Drawing.Pens.White, OrtaX, OrtaY, CurrentXsn, CurrentYsn)
        CurrentXsn = KoordinatX(Sn, Kadran, 30, OrtaX)
        CurrentYsn = KoordinatY(Sn, Kadran, 30, OrtaY)
        If Sn >= 360 Then Sn -= 360
        Sn += 6

        If Sn = 276 Then 'eğer saniye tam tur yapmışsa
            Dk += 6
            CurrentXdk = KoordinatX(Dk, Kadran, 50, OrtaX)
            CurrentYdk = KoordinatY(Dk, Kadran, 50, OrtaY)
            If Dk >= 360 Then Dk -= 360
        End If

        If Dk = 270 And Sn = 276 Then 'eğer dakika ve saniye tam tur yapmışsa
            Sa += 30
            CurrentXsa = KoordinatX(Sa, Kadran, 80, OrtaX)
            CurrentYsa = KoordinatY(Sa, Kadran, 80, OrtaY)
            If Sa >= 360 Then Sa -= 360
        End If
    End Sub
End Class


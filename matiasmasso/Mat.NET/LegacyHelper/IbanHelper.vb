Public Class IbanHelper
    Shared Function Img(sDigits As String, oLang As DTOLang, BlValidated As Boolean, oCountry As DTOCountry, oBank As DTOBank, oBranch As DTOBankBranch) As Byte()
        Dim IntWidth As Integer = 250
        Dim IntHeight As Integer = 50
        Dim retval As New Bitmap(IntWidth, IntHeight)
        If sDigits > "" Then
            Try
                Dim COLOR_INCOMPLETE As Color = Color.LightGoldenrodYellow
                Dim COLOR_PAIS_ES As Color = Color.LightBlue
                Dim COLOR_PAIS_NOT_ES As Color = Color.LightGreen
                Dim COLOR_NOTVALIDATED As Color = Color.LightSalmon

                Dim oColor As Color

                If IsNumeric(sDigits) Then
                    sDigits = DTOIban.Structure.IbanDigitsFromEspCcc(sDigits)
                End If
                If oCountry IsNot Nothing Then

                    If BlValidated Then
                        If oCountry.ISO = "ES" Then
                            oColor = COLOR_PAIS_ES
                        Else
                            oColor = COLOR_PAIS_NOT_ES
                        End If
                    Else
                        oColor = COLOR_INCOMPLETE
                        'If sDigits.Length = 24 Then ' New IbanStructure(oCountry).TotalLen Then
                        'oColor = COLOR_NOTVALIDATED
                        ' Else
                        'End If
                    End If

                    Dim linGrBrush As New System.Drawing.Drawing2D.LinearGradientBrush(
                       New Point(48, 1),
                       New Point(IntWidth, 1),
                       Color.White,
                       oColor)

                    Dim oLogo As Image = Nothing
                    If oBank IsNot Nothing Then oLogo = ImageHelper.FromBytes(oBank.Logo)

                    Dim oFont As New Font("Ms Sans Serif", 8)
                    Dim oGraphics As Graphics = Graphics.FromImage(retval)
                    Dim oWarn As Image = My.Resources.warn
                    Dim oRectangle As New Rectangle(0, 0, IntWidth - 1, IntHeight - 1)
                    With oGraphics
                        .FillRectangle(linGrBrush, oRectangle)
                        .DrawLine(Pens.Black, 0, 0, IntWidth, 0)
                        .DrawLine(Pens.Black, 0, 0, 0, IntHeight)
                        .DrawLine(Pens.White, 0, IntHeight, 0, IntHeight)
                        .DrawLine(Pens.White, IntWidth, 0, IntWidth, 0)
                        If Not oLogo Is Nothing Then
                            .DrawImage(oLogo, 1, 1)
                        End If
                        If Not BlValidated Then
                            .DrawImage(oWarn, IntWidth - 17, 1)
                        End If

                        If oBank IsNot Nothing Then
                            .DrawString(DTOBank.NomComercialORaoSocial(oBank), oFont, Brushes.Black, 50, 0)
                        End If

                        If oBranch IsNot Nothing Then
                            .DrawString(oBranch.Address, oFont, Brushes.Black, 50, 12)
                            If oBranch.Location IsNot Nothing Then
                                .DrawString(oBranch.Location.FullNom(oLang), oFont, Brushes.Black, 50, 24)
                            End If
                        End If

                        .DrawString(DTOIban.Formated(sDigits), oFont, Brushes.Black, 50, 36)
                    End With
                End If
            Catch ex As Exception
                'MailErr(ex.Message & " Iban.Digits = " & sDigits)
            End Try
        End If
        Return retval.Bytes()
    End Function

End Class

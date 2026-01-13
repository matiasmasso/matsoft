Public Class DTODocFile
    Implements iMedia
    Property hash As String Implements iMedia.Hash
    Property mime As MimeCods Implements iMedia.Mime
    Property length As Double Implements iMedia.Length
    Property size As Size Implements iMedia.Size
    Property hRes As Integer Implements iMedia.HRes
    Property vRes As Integer Implements iMedia.VRes
    Property pags As Integer Implements iMedia.Pags
    <JsonIgnore> Property Thumbnail As Image Implements iMedia.Thumbnail
    <JsonIgnore> Property Stream As Byte()
    Property filename As String
    Property fch As Date Implements iMedia.Fch
    Property nom As String
    Property obsolet As Boolean
    Property fchCreated As DateTime
    Property logCount As Integer

    Public Const THUMB_WIDTH As Integer = 350
    Public Const THUMB_HEIGHT As Integer = 400

    Public Enum Cods
        NotSet
        Correspondencia '1 Ok
        Assentament '2
        Hisenda '3 Ok
        Pdc '4
        Old_SellOutCsv '5 DEPRECATED
        SellOutExcel '6
        CliDoc
        Free8
        Download '9
        LogoVectorial
        IncidenciaDoc
        Cmr '12 a revisar
        PdcConfirm 'a revisar (obsolet des de 2017.09.28)
        PrAdDoc '14
        PrInsercio '15
        PrOrdreDeCompra '16
        Contracte '17 Ok
        Escriptura '18 Ok
        ExtracteBancari
        ArtPromo
        LiniaTelConsum
        RevistaPortada '22
        Flota
        TelMissatge
        DomiciliacioBancaria '25
        TutorialCertificat
        TpaEPubBook
        Dua
        ProductDownload
        StatSellOut '30
        Alb
        IncidenciesExcel
        IbanMandato
        TarifaExcel
        TarifaCsv
        OpenOrders
        PurchaseOrderExcel
        LlibreDiari
        LlibreMajor
        MaybornSalesExcel
        TransmisioAlbarans
        RepCertRetencio
        JSONSalePoints
        StocksExcel
        RawDataLast12MonthsCsv
        Vehicle
        RankingClients
        MediaResource
        RankingProducts
        ZipGalleryDownloads
        IbanMandatoManual
        Forecast
        SellOutPerChannel
        ExcelSalePoints
        XMLSalePoints
        SkuRefs
        ExcelCustomerDeliveries3yearsDetail
        MediaResourceThumbnail
        Mem
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(sHash As String)
        MyBase.New()
        _Hash = sHash
    End Sub





    Public Function DownloadUrl(Optional AbsoluteUrl As Boolean = False) As String
        Return DownloadUrl(_hash, AbsoluteUrl)
    End Function

    Shared Function DownloadUrl(hash As String, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If Not String.IsNullOrEmpty(hash) Then
            retval = MmoUrl.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.Download, CryptoHelper.StringToHexadecimal(hash))
        End If
        Return retval
    End Function


#Region "Features"

    Shared Function Features(oMedia As iMedia, Optional BlShort As Boolean = False) As String
        Dim sb As New System.Text.StringBuilder
        Select Case oMedia.Mime
            Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Bmp, MimeCods.Tif, MimeCods.Tiff, MimeCods.Png
                sb.AppendFormat("{0} {1} {2}", oMedia.Mime.ToString, FeatureFileSize(oMedia), FeatureImgDimensions(oMedia))
            Case MimeCods.Pdf, MimeCods.Xps
                sb.AppendFormat("{0} {1} {2} {3}", oMedia.Mime.ToString, FeatureFileSize(oMedia), FeaturePags(oMedia), FeaturePagDimensions(oMedia))
            Case MimeCods.Xls, MimeCods.Xlsx, MimeCods.Csv
                sb.AppendFormat("Excel {0} {1} cols x {2} filas", FeatureFileSize(oMedia), oMedia.Size.Width, oMedia.Size.Height)
            Case MimeCods.Doc, MimeCods.Docx
                sb.AppendFormat("Word {0}", FeatureFileSize(oMedia))
            Case MimeCods.Mpg
                sb.AppendFormat("Video MPG {0}", FeatureFileSize(oMedia))
            Case MimeCods.Wmv
                sb.AppendFormat("Wmv (video) {0}", FeatureFileSize(oMedia))
            Case MimeCods.Mp4
                sb.AppendFormat("Mp4 (video) {0}", FeatureFileSize(oMedia))
            Case MimeCods.Pla, MimeCods.Txt
                sb.AppendFormat("Txt {0}", FeatureFileSize(oMedia))
            Case MimeCods.Wav
                sb.AppendFormat("Wav (audio) {0}", FeatureFileSize(oMedia))
            Case MimeCods.Cer
                sb.AppendFormat("Cer (certificat) {0}", FeatureFileSize(oMedia))
            Case Else
                sb.AppendFormat("{0} {1}", oMedia.Mime.ToString, FeatureFileSize(oMedia))
        End Select
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function FeaturePags(oMedia As iMedia) As String

        Dim retval As String = ""
        Select Case oMedia.Pags
            Case 0
            Case 1
                retval = String.Format("1 pag.")
            Case Else
                retval = String.Format("{0} pags.", oMedia.Pags)
        End Select
        Return retval
    End Function

    Shared Function FeaturePagDimensions(oMedia As iMedia) As String
        Dim retval As String = ""
        If oMedia.Size.Width = 210 And oMedia.Size.Height = 297 Then
            retval = "DIN A4"
        Else
            retval = String.Format("{0}x{1} mm", oMedia.Size.Width, oMedia.Size.Height)
        End If
        Return retval
    End Function

    Shared Function FeatureImgDimensions(oMedia As iMedia) As String
        Dim sb As New Text.StringBuilder
        sb.AppendFormat("{0}x{1} px", oMedia.Size.Width, oMedia.Size.Height)
        If oMedia.HRes <> 0 Then
            sb.AppendFormat(" {0}", oMedia.HRes)
            If oMedia.VRes <> oMedia.HRes Then
                sb.AppendFormat("x{0}", oMedia.VRes)
            End If
            sb.Append(" ppp")

        End If
        Dim retval = sb.ToString
        Return retval
    End Function

    Shared Function FeatureFchCreated(oDocfile As DTODocFile) As String
        Return Format(oDocfile.fchCreated, "dd/MM/yy HH:mm")
    End Function

    Shared Function FeatureFileSize(oMedia As iMedia) As String
        Return FeatureFileSize(oMedia.Length)
    End Function

    Shared Function FeatureFileSize(dblBytes As Double) As String
        Dim sb As New Text.StringBuilder
        If dblBytes > 1000000000 Then
            sb.Append(Format(dblBytes / 1000000000, "#,##0.#") & " Gb")
        ElseIf dblBytes > 1000000 Then
            sb.Append(Format(dblBytes / 1000000, "#,##0.#") & " Mb")
        ElseIf dblBytes > 0 Then
            sb.Append(Format(dblBytes, "#,##0.#") & " Kb")
        End If
        Dim retval = sb.ToString
        Return retval
    End Function
#End Region




End Class




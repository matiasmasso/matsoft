
Imports Microsoft.Office.Interop

Public Module root

    Public Async Sub NewCliAlbNew(ByVal oCustomer As DTOCustomer)
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(oCustomer, exs) Then
            'check bloqueig
            If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                Dim oDelivery = FEB2.Delivery.Factory(exs, oCustomer, Current.Session.User, GlobalVariables.Emp.Mgz)
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                oFrm.Show()
            Else
                Dim oMsgBoxStyle As MsgBoxStyle = MsgBoxStyle.OkOnly
                Dim oRol As DTORol = Current.Session.User.Rol
                If oRol.IsAdmin Then oMsgBoxStyle = MsgBoxStyle.RetryCancel
                Dim rc As MsgBoxResult = MsgBox(exs(0).Message, oMsgBoxStyle, "MAT.NET")
                Select Case rc
                    Case MsgBoxResult.Retry
                    Case Else
                        Exit Sub
                End Select
            End If
        Else
            UIHelper.WarnError(exs)
        End If


    End Sub


    Public Async Sub ShowPurchaseOrder(ByVal oPurchaseOrder As DTOPurchaseOrder)
        Dim exs As New List(Of Exception)
        If FEB2.PurchaseOrder.Load(exs, oPurchaseOrder, GlobalVariables.Emp.Mgz) Then
            Select Case oPurchaseOrder.cod
                Case DTOPurchaseOrder.Codis.client
                    If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case Else
                    MsgBox("nomes implementades les comandes de client", MsgBoxStyle.Exclamation)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Public Sub NewMail(ByVal sTo As String)
        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            Try
                .Recipients.Add(sTo)
                .Display()
            Catch ex As Exception
            End Try
        End With
    End Sub

    Public Sub ImportFile()
        Dim oDlg As New System.Windows.Forms.OpenFileDialog
        With oDlg
            .Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
            .FilterIndex = 1
            If .ShowDialog() = DialogResult.OK Then
                Dim oFrm As New Frm_ImportMailbox(.FileName)
                oFrm.Show()
            End If
        End With
    End Sub

    Public Sub ImportMailBoxAttachments()
        Dim oFrm As New Frm_ImportMailbox
        With oFrm
            .Show()
        End With
    End Sub


    Public Sub ShowPqHdrs(Optional ByVal oMgz As DTOMgz = Nothing)
        'If oMgz Is Nothing Then oMgz = Current.Session.Emp.Mgz
        'Dim oFrm As New Frm_PqHdrs
        'oFrm.Mgz = oMgz
        'oFrm.Show()
    End Sub


    Public Sub ShowSelArt()
        Dim oFrm As New Frm_Art_Select
        oFrm.Show()
    End Sub


    Public Sub ExeCsaDespeses(ByVal oCsa As DTOCsa)
        MsgBox("aquest programa s'esta refent; si us plau passeu-me les dades de la remesa que voleu entrar")
        'Dim oFrm As New Frm_Csa_Despeses
        'With oFrm
        ' .Csa = oCsa
        ' .Show()
        ' End With
    End Sub

    Public Sub TabControlHideTabLabels(ByVal oXl As TabControl)
        Dim oParent As Control = oXl.Parent
        Dim oPBox As New PictureBox
        oParent.Controls.Add(oPBox)
        oXl.Appearance = TabAppearance.Buttons
        With oPBox
            .Height = 21
            .Width = oXl.Width
            .Top = oXl.Top
            .Left = oXl.Left
            .BringToFront()
        End With
    End Sub


    Public Sub Test()
        Winforms.Test.ShowForm()
    End Sub



    Public Sub ShowCsvFromDataset(ByVal oDs As DataSet, Optional ByVal sFileName As String = "", Optional ByVal sTitleRow As String = "")
        Dim sTmpFolder As String = FileSystemHelper.TmpFolder & "\"
        Dim oTmpFolder As System.IO.DirectoryInfo = FileSystemHelper.CleanOrCreateFolder(sTmpFolder)

        If sFileName > "" Then
            If Right(sFileName, 4) <> ".csv" Then sFileName = sFileName & ".csv"

            'carregat el fitxer si ja existeix
            'si no t'el pots carregar perque está ocupat, dona un nom aleatori
            Try
                Dim oFile As New IO.FileInfo(sTmpFolder & sFileName)
                If oFile.Exists() Then oFile.Delete()
            Catch ex As Exception
                sFileName = System.Guid.NewGuid.ToString & ".csv"
            End Try
        Else
            sFileName = System.Guid.NewGuid.ToString & ".csv"
        End If

        Dim sFullPath As String = oTmpFolder.FullName & sFileName

        Dim sr As New IO.StreamWriter(sFullPath, False, System.Text.Encoding.Default)
        If sTitleRow > "" Then
            sr.WriteLine(sTitleRow)
        End If

        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            Dim BlFirstCell As Boolean = True
            For Each oValue As Object In oRow.ItemArray
                If BlFirstCell Then
                    BlFirstCell = False
                Else
                    sr.Write(";")
                End If
                Dim s As String = ""
                If Not IsDBNull(oValue) Then
                    s = oValue.ToString
                End If
                sr.Write(s)
            Next
            sr.WriteLine()
        Next

        sr.Close()

        Process.Start(sFullPath)

    End Sub


    Public Sub ShowTrpFrasVivace()
        Dim oFrm As New Frm_Trp_FrasVivace
        With oFrm
            .Show()
        End With
    End Sub


    Public Sub ShowIngresosYDespesesMensual(Optional ByVal iYea As Integer = 0, Optional ByVal iMes As Integer = 0)
        If iYea = 0 Or iMes = 0 Then
            Dim DtFch As Date = Today.AddMonths(-1)
            iYea = DtFch.Year
            iMes = DtFch.Month
        End If

        'Dim oFrm As New Frm_Adm_IngresosYDespesesMensual
        'With oFrm
        ' .Yea = iYea
        ' .Mes = iMes
        ' .Show()
        ' End With
    End Sub


    Public Sub ShowPrvLastEntrys()
        Dim codis As New List(Of DTOPurchaseOrder.Codis)
        codis.Add(DTOPurchaseOrder.Codis.Proveidor)
        Dim oFrm As New Frm_Deliveries(codis)
        oFrm.Show()
    End Sub



    Public Sub SowAeatMods()
        'Dim oFrm As New Frm_Aeat_Mods
        Dim oFrm As New Frm_AeatModels
        oFrm.Show()
    End Sub

    Public Sub ShowFileImport(ByVal oDocFile As DTODocFile, Optional ByVal oContact As DTOContact = Nothing)
        Dim oFrm As New Frm_FileImport
        oFrm.DocFile = oDocFile
        oFrm.Contact = oContact
        oFrm.Show()
    End Sub

    Public Sub ClearAlbBloqueig()
        Dim oFrm As New Frm_AlbBloqueigs
        oFrm.Show()
    End Sub

    Public Async Function RepRetencionsSave(ByVal iYea As Integer, ByVal iQuarter As Integer, exs As List(Of Exception)) As Task(Of Boolean)

        Dim iStartMes As Integer = iQuarter * 3 - 2
        Dim DtFirstDayFirstMonth As New Date(iYea, iStartMes, 1)
        Dim DtLastDayLastMonth As Date = DtFirstDayFirstMonth.AddMonths(3).AddDays(-1)
        Dim sFchStart As String = Format(DtFirstDayFirstMonth, "yyyyMMdd")
        Dim sFchEnd As String = Format(DtLastDayLastMonth, "yyyyMMdd")

        Dim oReps = Await FEB2.Reps.WithRetencions(exs, Current.Session.Emp, DtFirstDayFirstMonth, DtLastDayLastMonth)

        Dim oPdfRepRetencio As LegacyHelper.PdfRepRetencions = Nothing
        Dim oStream As Byte() = Nothing
        For Each oRep In oReps
            Dim oRepLiqs As List(Of DTORepLiq) = Await FEB2.Repliqs.GetRepLiqsFromQuarter(exs, oRep, iYea, iQuarter)
            oPdfRepRetencio = New LegacyHelper.PdfRepRetencions(Current.Session.User, oRepLiqs, oRep, iYea, iQuarter)
            Dim oCert = Await FEB2.Cert.Find(GlobalVariables.Emp.Org, exs)
            oStream = DirectCast(oPdfRepRetencio.Pdf, LegacyHelper.C1PdfHelper.Document).SignedStream(exs, oCert.Stream, oCert.Pwd)

            Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oStream, MimeCods.Pdf)
            If exs.Count = 0 Then
                Dim oDoc As New DTOContactDoc()
                With oDoc
                    .Contact = oRep
                    .Ref = iYea.ToString & "." & iQuarter.ToString
                    .Fch = DtLastDayLastMonth
                    .Type = DTOContactDoc.Types.Retencions
                    .DocFile = oDocFile
                End With
                If Await FEB2.ContactDoc.Update(oDoc, exs) Then
                    'emailRepRetencions(oDoc)
                End If
            End If
        Next
        Return oReps.Count
    End Function


    Public Sub ShowEanEci()
        Dim oFrm As New Frm_Ean_ECI
        oFrm.Show()
    End Sub

    Public Sub ShowWebLog()
        Dim oFrm As New Frm_WebLog
        oFrm.Show()
    End Sub


    Public Sub ShowCodisMercancia()
        Dim oFrm As New Frm_CodisMercancia
        oFrm.Show()
    End Sub


    Public Sub ShowCnaps()
        Dim oFrm As New Frm_Cnaps
        oFrm.Show()
    End Sub


    Public Sub ShowLiniesTelefon()
        Dim oFrm As New Frm_LiniesTelefon
        oFrm.Show()
    End Sub

    Public Sub ShowTelConsums()
        Dim oFrm As New Frm_LiniaTelConsumsXMes
        oFrm.Show()
    End Sub

    Public Sub ShowTaxes()
        Dim oFrm As New Frm_Taxes
        oFrm.Show()
    End Sub

    Public Sub ShowCliCredits()
        'Dim oFrm As New Frm_CliCredits
        Dim oFrm As New Frm_CreditsLastAlbs
        oFrm.Show()
    End Sub

    Public Function GetIcoFromMime(ByVal oMimeCod As MimeCods) As Image
        Dim retVal As Image = Nothing
        Select Case oMimeCod
            Case MimeCods.Pdf
                retVal = My.Resources.pdf
            Case MimeCods.Xps
                retVal = My.Resources.xps
            Case MimeCods.Doc, MimeCods.Docx
                retVal = My.Resources.word
            Case MimeCods.Xls, MimeCods.Xlsx
                retVal = My.Resources.Excel
            Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Bmp, MimeCods.Png, MimeCods.Tif, MimeCods.Tiff, MimeCods.Ai, MimeCods.Eps
                retVal = My.Resources.img_16
            Case MimeCods.Zip
                retVal = My.Resources.Zip
            Case Else
                retVal = My.Resources.empty
        End Select
        Return retVal
    End Function

    Private Function FilesDroppedFromOutlookMessageExists(ByVal e As System.Windows.Forms.DragEventArgs) As Boolean
        Dim retval As Boolean = e.Data.GetDataPresent("FileGroupDescriptor")
        Return retval
    End Function


    Public Sub ShowCnapsStat()
        'Dim oFrm As New Frm_CnapsStat
        'oFrm.Show()
    End Sub




    Public Sub ShowEciPdcs()
        'Dim oFrm As New Frm_ECI_Pdcs
        Dim oFrm As New Frm_EciPurchaseOrders
        oFrm.Show()
    End Sub


    Public Sub ShowBlog()
        Dim oFrm As New Frm_Blog
        oFrm.Show()
    End Sub


    Public Sub ShowLiteral(ByVal sCaption As String, ByVal sText As String)
        Dim oFrm As New Frm_Literal(sCaption, sText)
        oFrm.Show()
    End Sub


End Module


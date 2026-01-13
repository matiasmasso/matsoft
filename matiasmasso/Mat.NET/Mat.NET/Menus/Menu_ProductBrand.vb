Imports Microsoft.Office.Interop

Public Class Menu_ProductBrand
    Inherits Menu_Base

    Private mTpas As Tpas
    Private _Brands As List(Of DTOProductBrand)

#Region "Constructors"

    Public Sub New(ByVal oBrand As DTOProductBrand, Optional ByVal oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Brands = New List(Of DTOProductBrand)
        _Brands.Add(oBrand)

        mTpas = New Tpas
        mTpas.Add(New Tpa(oBrand.Guid))
    End Sub

    Public Sub New(ByVal oBrands As List(Of DTOProductBrand))
        MyBase.New()
        _Brands = oBrands
        mTpas = New Tpas
        For Each oBrand As DTOProductBrand In _Brands
            mTpas.Add(New Tpa(oBrand.Guid))
        Next
    End Sub


#End Region


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_ArtsXMes(), _
        MenuItem_Ranking(), _
        MenuItem_Forecast(), _
        MenuItem_Delete(), _
        MenuItem_Atlas(), _
        MenuItem_Tarifes(), _
        MenuItem_ePubBook(), _
        MenuItem_ePubBook_Update(), _
        MenuItem_Repeticions(), _
        MenuItem_MailReps(), _
        MenuItem_RepAdrs(), _
        MenuItem_Sorteos(), _
        MenuItem_emailsDistribuidorsOficials(), _
        MenuItem_Incidencies()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_ArtsXMes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "vendes articles/mes"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ArtsXMes
        Return oMenuItem
    End Function

    Private Function MenuItem_Ranking() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ranking"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_StpxTpa
        Return oMenuItem
    End Function

    Private Function MenuItem_Forecast() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Forecast"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Forecast
        Return oMenuItem
    End Function


    Private Function MenuItem_Atlas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Atlas"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Atlas
        Return oMenuItem
    End Function

    Private Function MenuItem_Repeticions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Repeticions"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Repeticions
        Return oMenuItem
    End Function

    Private Function MenuItem_Tarifes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Tarifes"
        oMenuItem.DropDownItems.Add("cost. copiar link", Nothing, AddressOf Do_CopyTarifaCost)
        oMenuItem.DropDownItems.Add("PVP. copiar link", Nothing, AddressOf Do_CopyTarifaPvp)
        oMenuItem.DropDownItems.Add("a una data. copiar link", Nothing, AddressOf Do_CopyTarifas)
        If BLL.BLLSession.Current.User.Rol.IsAdmin Then
            oMenuItem.DropDownItems.Add("dtes.s/compres", Nothing, AddressOf Do_CostDtos)
        End If

        Return oMenuItem
    End Function

    Private Function MenuItem_MailReps() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "circular a reps"
        'oMenuItem.Image = My.Resources.SquareArrowTurquesa
        AddHandler oMenuItem.Click, AddressOf Do_MailReps
        Return oMenuItem
    End Function

    Private Function MenuItem_RepAdrs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "adreces reps"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_RepAdrs
        Return oMenuItem
    End Function

    Private Function MenuItem_Sorteos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "sortejos"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Sorteos
        Return oMenuItem
    End Function

    Private Function MenuItem_ePubBook() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eBook"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ePubBook
        Return oMenuItem
    End Function

    Private Function MenuItem_ePubBook_Update() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eBook update"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ePubBookUpdate
        Return oMenuItem
    End Function

    Private Function MenuItem_emailsDistribuidorsOficials() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "emails Distr.Oficials"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_CsvEmailsOficialDistributors
        Return oMenuItem
    End Function

    Private Sub Do_CopyTarifaCost(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("revisar Matias")
        'Clipboard.SetDataObject(mTpas(0).PdfUrlTarifa(Now), True)
    End Sub

    Private Sub Do_CopyTarifaPvp(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("revisar Matias")

        'Clipboard.SetDataObject(mTpas(0).PdfUrlPvr(Now), True)
    End Sub

    Private Sub Do_CopyTarifas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DtFch As Date = Today
        Dim sFch As String = InputBox("tarifes a una data posterior:", "COPIAR ENLLAÇ A PAGINA DE TARIFES", DtFch.ToShortDateString)
        If IsDate(sFch) Then
            Dim sUrl As String = BLL_Product.TarifaUrl(True, CDate(sFch))
            Clipboard.SetDataObject(sUrl)
            MsgBox("copiat " & sUrl, MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("format de data & '" & sFch & "' incorrecte (dd/mm/aa)", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub


    Private Sub Do_CostDtos(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CostDtos(mTpas(0))
        oFrm.Show()
    End Sub

    Private Function MenuItem_Incidencies() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Incidencies"
        oMenuItem.Image = My.Resources.Spv
        AddHandler oMenuItem.Click, AddressOf Do_Incidencies
        Return oMenuItem
    End Function


    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_ZoomNew(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_ProductBrand(_ProductBrand)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Brands(0).Nom & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLProductBrand.Delete(_Brands(0), exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar la marca")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Tpa(mTpas(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ArtsXMes(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Stat_Arts_Mes
        With oFrm
            .Tpa = mTpas(0)
            .Show()
        End With
    End Sub

    Private Sub Do_StpxTpa(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowStpxTpaRanking(mTpas(0))
    End Sub

    Private Sub Do_Forecast(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowForecast(mTpas(0))
    End Sub

    Private Sub Do_Atlas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oBrand As New ProductBrand(mTpas(0).Guid)
        oBrand.Nom = mTpas(0).Nom

        Dim oStat As New DTOStat
        With oStat
            .Lang = BLL.BLLApp.Lang
            .Product = _Brands(0)
            .ConceptType = DTOStat.ConceptTypes.Geo
            .ExpandToLevel = 5
        End With
        Dim oFrm As New Frm_Stats(oStat)
        oFrm.Show()
        'root.ShowStatGeoMes(mTpas(0))
    End Sub

    Private Sub Do_Incidencies()
        Dim oQuery As DTOIncidenciaQuery = BLL_Incidencies.DefaultQuery(BLL.BLLSession.Current.User)
        With oQuery
            .Src = DTOIncidencia.Srcs.Producte
            .Product = New DTOProductBrand(mTpas(0).Guid)
        End With
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Private Sub Do_Repeticions()
        Dim oFrm As New Frm_ArtXStp_Repeticions(New Product(mTpas(0)))
        oFrm.Show()
    End Sub

    Private Sub Do_Sorteos()
        'Dim oFrm As New Frm_Sorteos(mTpas(0))
        'oFrm.Show()
    End Sub

    Private Sub Do_ePubBook(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oBook As ePubBook = mTpas(0).ePubBook
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "GUARDAR E-BOOK"
            .Filter = "ePub (*.ePub)|*.ePub|tots (*.*)|*.*"
            .DefaultExt = "ePub"
            .FileName = mTpas(0).Nom
            If .ShowDialog = DialogResult.OK Then
                oBook.Save(.FileName)
            End If
        End With
    End Sub

    Private Sub Do_ePubBookUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        mTpas(0).EPubBookUpdate()
        MsgBox("cataleg actualitzat", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub Do_CsvEmailsOficialDistributors()
        Dim oBrand As DTOProductBrand = _Brands(0)
        BLL.BLLProductBrand.Load(oBrand)
        Dim oEmails As List(Of DTOEmail) = BLL.BLLCliProductsBlocked.DistribuidorsOficialsActiveEmails(oBrand)
        Dim oCsv As DTOCsv = BLL.BLLCsv.NewCsv()
        For Each item As DTOEmail In oEmails
            Dim oRow As DTO.DTOCsvRow = BLL.BLLCsv.AddRow(oCsv)
            BLL.BLLCsv.AddCell(oRow, item.EmailAddress)
        Next
        UIHelper.SaveCsvDialog(oCsv, "emails distribuidors oficials " & oBrand.Nom)
    End Sub

    Private Sub Do_MailReps()
        Dim oProduct As DTOProduct = _Brands.First
        Dim sProductNom As String = BLL.BLLProduct.FullNom(oProduct)

        Dim oRecipients As New List(Of String)
        oRecipients.Add("a todos los representantes de " & sProductNom & " <info@matiasmasso.es>")

        Dim oBccs As New List(Of String)
        Dim oReps As List(Of DTORep) = BLL.BLLReps.All(oProduct)
        For Each oRep In oReps
            Dim oEmails As List(Of DTOEmail) = BLL.BLLEmails.All(oRep)
            If oEmails.Count > 0 Then
                oBccs.Add(oEmails(0).EmailAddress)
            End If
        Next

        Dim sSubject As String = "Circular a la red comercial de " & sProductNom
        OutlookHelper.NewMessage(oRecipients, , oBccs, sSubject)
    End Sub

    Private Sub Do_RepAdrs()
        Dim sb As New System.Text.StringBuilder
        For Each oRep As Rep In mTpas(0).Reps
            sb.Append(oRep.RazonSocialFacturacio)
            sb.Append(";")
            sb.Append(oRep.Adr.Text)
            sb.Append(";")
            sb.Append(oRep.Adr.Zip.ZipyCit)
            sb.Append(";")
            sb.Append(oRep.Movil(True))
            sb.AppendLine()
        Next

        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = "csv"
            .AddExtension = True
            .FileName = "adreces comercials " & mTpas(0).Nom
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = mTpas(0).Nom & ": guardar relació de noms i adreces dels comercials"
            Dim Rc As DialogResult = .ShowDialog()
            If Rc = Windows.Forms.DialogResult.OK Then
                Dim sr As New IO.StreamWriter(.FileName, False, System.Text.Encoding.Default)
                sr.WriteLine(sb.ToString)
                sr.Close()
            End If
        End With

    End Sub


End Class



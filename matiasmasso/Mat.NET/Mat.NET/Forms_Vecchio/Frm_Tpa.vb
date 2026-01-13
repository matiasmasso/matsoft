
Imports Microsoft.Office.Interop

Public Class Frm_Tpa

    Private mTpa As MaxiSrvr.Tpa
    Private mDsReps As DataSet
    Private mDsClis As DataSet
    Private mCancel As Boolean
    Private mChanged As Boolean
    Private mDefaultTab As Tabs = Tabs.General
    Private mDirtyWebThumbnail As Boolean
    Private mDirtyLogoVectorial As Boolean
    Private mDirtyLogoDistribuidorOficial As Boolean
    Private mInclouDownloadObsolets As Boolean
    Private mDsDownloads As DataSet
    Private mAllowEvents As Boolean
    Private mAllowEventsDownload As Boolean
    Private mAllowUpdate As Boolean
    Private mDirtyEBook As Boolean = False
    Private mLastSelectedPictureBox As PictureBox
    Private CleanTab(20) As Boolean
    Private mSelectionMode As BLL.Defaults.SelectionModes

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterSelect(ByVal sender As Object, ByVal e As MatEventArgs)


    Public Enum Tabs
        General
        Stps
        Clis
        Zonas
        Reps
        Web
        Downloads
        Logistica
        EBook
        Coleccions
    End Enum


    Private Enum ColsZonas
        IsoPais
        Zona
        RepId
        RepNom
    End Enum

    Public Sub New(ByVal oTpa As Tpa, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        mTpa = oTpa
        mSelectionMode = oSelectionMode
        refresca()
    End Sub



    Private Sub refresca()
        With mTpa
            TextBoxNom.Text = .Nom

            TextBoxTagLine_ESP.Text = .Tagline_ESP
            TextBoxTagLine_CAT.Text = .Tagline_CAT
            TextBoxTagLine_ENG.Text = .Tagline_ENG

            TextBoxDsc.Text = .Descripcio
            If Not .Proveidor Is Nothing Then Xl_ContactProveidor.Contact = .Proveidor
            CheckBoxShowAtlas.Checked = .ShowAtlas
            CheckBoxDistribuidorsOficials.Checked = .CodDist
            CheckBoxExclouDeServeiTecnic.Checked = .ExclouDeServeiTecnic
            CheckBoxObsoleto.Checked = .Obsoleto
            Xl_Image_Logo.Bitmap = .Image
            Xl_Color1.Color = .Color
            LoadStps()
            If mTpa.Id = 0 Then
                Me.Text = "NOVA MARCA COMERCIAL"
            Else
                Me.Text = mTpa.Nom & " (#" & mTpa.Id & ")"
            End If
            CheckReps()

            CheckBoxWebEnabledPro.Checked = .WebEnabledPro
            CheckBoxWebEnabledConsumer.Checked = .WebEnabledConsumer
            GroupBoxWebEnabledConsumer.Enabled = CheckBoxWebEnabledConsumer.Checked

            If .WebPortadaFrom = Date.MinValue Then
                CheckBoxWebPortadaFrom.Checked = False
                DateTimePickerWebPortadaFrom.Visible = False
                CheckBoxWebPortadaTo.Checked = False
                DateTimePickerWebPortadaTo.Visible = False
            Else
                CheckBoxWebPortadaFrom.Checked = True
                DateTimePickerWebPortadaFrom.Visible = True
                DateTimePickerWebPortadaFrom.Value = .WebPortadaFrom

                If .WebPortadaTo = Date.MinValue Then
                    CheckBoxWebPortadaTo.Checked = False
                    DateTimePickerWebPortadaTo.Visible = False
                Else
                    CheckBoxWebPortadaTo.Checked = True
                    DateTimePickerWebPortadaTo.Visible = True
                    DateTimePickerWebPortadaTo.Value = .WebPortadaTo
                End If
            End If


            CheckBoxLinkToManufacturer.Checked = .LinkToManufacturer
            Xl_Image_WebThumbnail.Bitmap = .WebThumbnail
            Xl_ImageLogoDistribuidorOficial.Bitmap = .LogoDistribuidorOficial
            If .LogoVectorial IsNot Nothing Then
                If .LogoVectorial.Exists Then
                    Xl_BigFileLogoVectorial.BigFile = .LogoVectorial.BigFile
                Else
                    'Xl_BigFileLogoVectorial.BigFile = New BigFileSrc(DTODocFile.Cods.LogoVectorial, Guid.NewGuid)
                End If
            Else
                'Xl_BigFileLogoVectorial.BigFile = New maxisrvr.BigFile(Guid.NewGuid, maxisrvr.BigFile.SrcCods.LogoVectorial)
            End If

            Xl_ImageJumbotron.EmptyImageLabelText = "importar 1.600x450"
            Xl_ImageJumbotron.Bitmap = .JumbotronImage

            Xl_CodiMercancia1.CodiMercancia = .CodiMercancia

            If .Mgz Is Nothing Then
                RadioButtonMgzDefault.Checked = True
                Xl_Mgzs_ComboBox1.Mgz = New Mgz(BLL.BLLApp.Mgz.Guid)
                Xl_Mgzs_ComboBox1.Enabled = False
            Else
                RadioButtonMgzEspecific.Checked = True
                Xl_Mgzs_ComboBox1.Mgz = .Mgz
                Xl_Mgzs_ComboBox1.Enabled = True
            End If

            Select Case .CodStk
                Case MaxiSrvr.Tpa.CodStks.Extern
                    RadioButtonCodStkExtern.Checked = True
                Case Else
                    RadioButtonCodStkIntern.Checked = True
            End Select

            mAllowEvents = True
        End With

        mAllowUpdate = BLL.BLLRol.AllowWrite(BLL.BLLSession.Current.User.Rol, "Cataleg")
        ButtonDel.Enabled = mAllowUpdate

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        mCancel = True
        mChanged = False
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mChanged Then
            If Not mCancel Then
                With mTpa
                    .Nom = TextBoxNom.Text

                    .Tagline_ESP = TextBoxTagLine_ESP.Text
                    .Tagline_CAT = TextBoxTagLine_CAT.Text
                    .Tagline_ENG = TextBoxTagLine_ENG.Text

                    .Descripcio = TextBoxDsc.Text
                    If Xl_ContactProveidor.Contact IsNot Nothing Then
                        .Proveidor = New Proveidor(Xl_ContactProveidor.Contact.Guid)
                    End If
                    .ShowAtlas = CheckBoxShowAtlas.Checked
                    .CodDist = IIf(CheckBoxDistribuidorsOficials.Checked, DTOProductBrand.CodDists.DistribuidorsOficials, DTOProductBrand.CodDists.Free)
                    .ExclouDeServeiTecnic = CheckBoxExclouDeServeiTecnic.Checked
                    .Obsoleto = CheckBoxObsoleto.Checked
                    .DefaultStp = mDefaultStp
                    .Color = Xl_Color1.Color
                    'ImageList1.Images.Add("WARN", My.Resources.warn)
                    .WebEnabledPro = CheckBoxWebEnabledPro.Checked
                    .WebEnabledConsumer = CheckBoxWebEnabledConsumer.Checked
                    If CheckBoxWebPortadaFrom.Checked Then
                        Dim DtFrom As Date = DateTimePickerWebPortadaFrom.Value
                        .WebPortadaFrom = New Date(DtFrom.Year, DtFrom.Month, DtFrom.Day, 0, 0, 0)
                    Else
                        .WebPortadaFrom = Date.MinValue
                    End If
                    If CheckBoxWebPortadaTo.Checked Then
                        Dim DtTo As Date = DateTimePickerWebPortadaTo.Value
                        .WebPortadaTo = New Date(DtTo.Year, DtTo.Month, DtTo.Day, 23, 59, 59)
                    Else
                        .WebPortadaTo = Date.MinValue
                    End If
                    .LinkToManufacturer = CheckBoxLinkToManufacturer.Checked
                    .CodiMercancia = Xl_CodiMercancia1.CodiMercancia
                    If Xl_Mgzs_ComboBox1.Mgz.Id = Mgz.DefaultMgz.Id Then
                        .Mgz = MaxiSrvr.Mgz.FromNum(.Emp, 0)
                    Else
                        .Mgz = Xl_Mgzs_ComboBox1.Mgz
                    End If

                    If RadioButtonCodStkExtern.Checked Then
                        .CodStk = MaxiSrvr.Tpa.CodStks.Extern
                    Else
                        .CodStk = MaxiSrvr.Tpa.CodStks.Intern
                    End If

                    If Xl_ImageJumbotron.IsDirty Then
                        .JumbotronImage = Xl_ImageJumbotron.Bitmap
                    End If

                    If mDirtyLogoVectorial Then
                        Dim oLogoVectorial = New BigFileSrc(DTODocFile.Cods.LogoVectorial)
                        oLogoVectorial.BigFile = Xl_BigFileLogoVectorial.BigFile
                        .LogoVectorial = oLogoVectorial
                    End If

                    If mDirtyWebThumbnail Then
                        .WebThumbnail = Xl_Image_WebThumbnail.Bitmap
                    End If

                    If mDirtyLogoDistribuidorOficial Then
                        .LogoDistribuidorOficial = Xl_ImageLogoDistribuidorOficial.Bitmap
                    End If


                    If mDirtyEBook Then
                        .ImgPortadaEBook = Xl_ImageEBook.Bitmap
                    End If

                    .Update()
                End With
            End If

            RaiseEvent AfterUpdate(Me, New MatEventArgs(mTpa))
        End If
        Me.Close()
    End Sub

    Private Function GetDownloadsFromGrid() As Downloads
        Dim oDownloads As New Downloads
        '*******************************************
        Return oDownloads
    End Function

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If mTpa.AllowDelete Then
            Dim rc As MsgBoxResult = MsgBox("eliminem " & mTpa.Nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                mTpa.Delete()
                MsgBox(mTpa.Nom & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(mTpa, New System.EventArgs)
                Me.Close()
            Else
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
            End If
        Else
            MsgBox(mTpa.Nom & " no esta buida." & vbCrLf & "Operació cancelada.", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub SetDirty()
        If mAllowEvents Then
            Dim BlEnableButtons As Boolean = True
            If Not mAllowUpdate Then BlEnableButtons = False
            If Not mAllowEvents Then BlEnableButtons = False

            If BlEnableButtons Then
                mChanged = True
                ButtonOk.Enabled = True
            End If
        End If
    End Sub

    Private Sub Xl_Image_Logo_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Image_Logo.AfterUpdate
        Dim BlEnableButtons As Boolean = True
        If Not mAllowUpdate Then BlEnableButtons = False
        If Not mAllowEvents Then BlEnableButtons = False

        If BlEnableButtons Then
            mTpa.Image = Xl_Image_Logo.Bitmap
            Xl_Image_Logo.Bitmap = maxisrvr.GetThumbnail(mTpa.Image, Xl_Image_Logo.Width, Xl_Image_Logo.Height)
            SetDirty()
        End If
    End Sub

    Private Sub Xl_Image_WebThumbnail_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Image_WebThumbnail.AfterUpdate
        mDirtyWebThumbnail = True
        SetDirty()
    End Sub

    Private Sub Xl_ImageLogoDistribuidorOficial_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_ImageLogoDistribuidorOficial.AfterUpdate
        mDirtyLogoDistribuidorOficial = True
        SetDirty()
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     CheckBoxObsoleto.CheckedChanged, _
      CheckBoxExclouDeServeiTecnic.CheckedChanged, _
      Xl_ContactProveidor.AfterUpdate, _
       TextBoxNom.TextChanged, _
        TextBoxDsc.TextChanged, _
        CheckBoxWebEnabledPro.CheckedChanged, _
           DateTimePickerWebPortadaFrom.ValueChanged, _
             DateTimePickerWebPortadaTo.ValueChanged, _
              CheckBoxLinkToManufacturer.CheckedChanged, _
               CheckBoxShowAtlas.CheckedChanged, _
                CheckBoxDistribuidorsOficials.CheckedChanged, _
                 TextBoxTagLine_ESP.TextChanged, _
                  TextBoxTagLine_CAT.TextChanged, _
                   TextBoxTagLine_ENG.TextChanged, _
                    Xl_ImageJumbotron.AfterUpdate

        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub Xl_Color1_AfterUpdate(ByVal oColor As System.Drawing.Color) Handles _
    Xl_Color1.AfterUpdate
        SetDirty()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim iTab As Tabs = TabControl1.SelectedIndex
        If Not CleanTab(iTab) Then
            Select Case iTab
                Case Tabs.Stps
                    mDefaultStp = mTpa.DefaultStp
                    RefreshRequestStps(Nothing, New System.EventArgs)
                Case Tabs.Clis
                    RefreshRequestClis(Nothing, New System.EventArgs)
                Case Tabs.Zonas
                    LoadGridZonas()
                Case Tabs.Reps
                    LoadGridReps()
                Case Tabs.Web
                Case Tabs.Downloads
                    LoadProductDownloads()
                Case Tabs.Logistica
                Case Tabs.EBook
                    Xl_ImageEBook.Bitmap = mTpa.ImgPortadaEBook
                Case Tabs.Coleccions
                    Xl_TpaColeccions1.SelectionMode = mSelectionMode
                    Xl_TpaColeccions1.Tpa = mTpa
            End Select
            CleanTab(iTab) = True
        End If

    End Sub

#Region "Clis"

    Public Enum Clis
        Pais
        Zon
        Cit
        Cod
        Ico
        Cli
        Nom
    End Enum

    Private Sub LoadGridClis()
        Dim SQL As String = "SELECT CIT.ISOpais, ZON.Zona, CIT.Cit, CLITPA.Cod, CliGral.Cli, (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom ELSE CliGral.RaoSocial END) As CliNom " _
        & "FROM ZON INNER JOIN " _
        & "CIT ON ZON.IsoPais = CIT.ISOpais AND ZON.Id = CIT.Zona INNER JOIN " _
        & "CLITPA INNER JOIN " _
        & "CliAdr ON CLITPA.emp = CliAdr.emp AND CLITPA.Cli = CliAdr.cli ON CIT.Id = CliAdr.CitNum INNER JOIN " _
        & "CliGral ON CLITPA.emp = CliGral.emp AND CLITPA.Cli = CliGral.Cli " _
        & "WHERE CLITPA.emp =" & mTpa.emp.Id & " AND " _
        & "CLITPA.ProductGuid ='" & mTpa.Guid.ToString & "' "

        Dim oArray As New ArrayList
        If CheckBoxExclos.Checked Then
            oArray.Add("CLITPA.COD=3")
        End If
        If CheckBoxNA.Checked Then
            oArray.Add("CLITPA.COD=2")
        End If
        If CheckBoxExclusiva.Checked Then
            oArray.Add("CLITPA.COD=1")
        End If

        If oArray.Count > 0 Then
            SQL = SQL & " AND (" & oArray(0)
            Dim i As Integer
            For i = 1 To oArray.Count - 1
                SQL = SQL & " OR " & oArray(i)
            Next
            SQL = SQL & ") "
        End If

        SQL = SQL & "ORDER BY CIT.ISOpais, ZON.Zona, CIT.Cit, CliGral.RaoSocial"
        mDsClis = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsClis.Tables(0)

        'crea columna icono imatge
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Clis.Ico)

        With DataGridViewClis
            With .RowTemplate
                .Height = DataGridViewClis.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DataSource = oTb
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .BackgroundColor = Color.White

            With .Columns(Clis.Pais)
                .Visible = False
            End With
            With .Columns(Clis.Zon)
                .HeaderText = "zona"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Clis.Cit)
                .HeaderText = "població"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Clis.Cod)
                .Visible = False
            End With
            With .Columns(Clis.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Clis.Cli)
                .Visible = False
            End With
            With .Columns(Clis.Nom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub


    Private Sub DataGridViewClis_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewClis.CellFormatting
        Select Case e.ColumnIndex
            Case Clis.Ico
                Dim oRow As DataGridViewRow = DataGridViewClis.Rows(e.RowIndex)
                Select Case oRow.Cells(Clis.Cod).Value
                    Case DTO.DTOCliProductBlocked.Codis.Standard
                        e.Value = My.Resources.Ok
                    Case DTO.DTOCliProductBlocked.Codis.Exclusiva
                        e.Value = My.Resources.star
                    Case DTO.DTOCliProductBlocked.Codis.NoAplicable
                        e.Value = My.Resources.NoPark
                    Case DTO.DTOCliProductBlocked.Codis.Exclos
                        e.Value = My.Resources.wrong
                    Case DTO.DTOCliProductBlocked.Codis.AltresEnExclusiva
                        e.Value = My.Resources.warn
                    Case DTO.DTOCliProductBlocked.Codis.DistribuidorOficial
                        e.Value = My.Resources.medalla
                End Select
        End Select
    End Sub

    Private Function CurrentCli() As Contact
        Dim oRow As DataGridViewRow = DataGridViewClis.CurrentRow
        Dim CliId As Long = oRow.Cells(Clis.Cli).Value
        Dim oCli As Contact = MaxiSrvr.Contact.FromNum(mTpa.emp, CliId)
        Return oCli
    End Function

    Private Sub RefreshRequestClis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        If DataGridViewClis.Rows.Count > 0 Then
            i = DataGridViewClis.CurrentRow.Index
            j = DataGridViewClis.CurrentCell.ColumnIndex
            iFirstRow = DataGridViewClis.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridClis()

        If DataGridViewClis.Rows.Count > 0 Then
            DataGridViewClis.FirstDisplayedScrollingRowIndex() = iFirstRow
            If i > DataGridViewClis.Rows.Count - 1 Then
                DataGridViewClis.CurrentCell = DataGridViewClis.Rows(DataGridViewClis.Rows.Count - 1).Cells(j)
            Else
                DataGridViewClis.CurrentCell = DataGridViewClis.Rows(i).Cells(Clis.Nom)
            End If
            SetContextMenuClis()
        End If


    End Sub

    Private Sub SetContextMenuClis()
        Dim oContextMenu As New ContextMenuStrip

        If DataGridViewClis.CurrentRow IsNot Nothing Then
            Dim iRowIndex As Integer = DataGridViewClis.CurrentRow.Index
            Dim oContact As Contact = CurrentCli()


            If oContact IsNot Nothing Then
                oContextMenu.Items.Add("Zoom", Nothing, AddressOf Do_ZoomCliTpa)
                Dim oMenuItem As ToolStripMenuItem = oContextMenu.Items.Add("client")
                Dim oMenu_Contact As New Menu_Contact(oContact)
                AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequestClis
                oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)

                oContextMenu.Items.Add("-")
            End If

            oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_ExcelClis)
        End If

        DataGridViewClis.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Cli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxExclos.CheckedChanged, _
     CheckBoxNA.CheckedChanged, _
      CheckBoxExclusiva.CheckedChanged
        RefreshRequestClis(sender, e)
    End Sub

    Private Sub DataGridViewClis_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridViewClis.DoubleClick
        Do_ZoomCliTpa()
    End Sub


    Private Sub DataGridViewClis_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewClis.SelectionChanged
        SetContextMenuClis()
    End Sub

    Private Sub Do_ZoomCliTpa()
        Dim oProduct As DTOProduct = BLL.BLLProduct.Find(mTpa.Guid)
        Dim oContact As DTOContact = BLL.BLLContact.Find(CurrentCli.Guid)
        Dim oCliProductBlocked As DTO.DTOCliProductBlocked = BLL.BLLCliProductBlocked.Find(oContact, oProduct)
        Dim oFrm As New Frm_CliProductBlocked(oCliProductBlocked)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestClis
        oFrm.Show()
    End Sub

    Private Sub Do_ExcelClis()
        MatExcel.GetExcelFromDataGridView(DataGridViewClis).Visible = True
    End Sub
#End Region

#Region "Reps"

    Public Enum RepCols
        Warn
        Ico
        Pais
        ZonId
        RepId
        ZonNom
        RepNom
    End Enum

    Private Sub CheckReps() 'As Boolean
        Dim SQL As String = "SELECT TOP 1 COUNT(DISTINCT REPZON.REP) AS REPES " _
        & "FROM CliRep INNER JOIN " _
        & "RepZon ON CliRep.emp = RepZon.emp AND CliRep.cli = RepZon.rep RIGHT OUTER JOIN " _
        & "TPAZON INNER JOIN " _
        & "ZON ON TPAZON.ISOpais = ZON.IsoPais AND TPAZON.zon = ZON.Id ON RepZon.emp = TPAZON.emp AND RepZon.ISOpais = TPAZON.ISOpais AND RepZon.zon = TPAZON.zon And RepZon.tpa = TPAZON.tpa " _
        & "WHERE TPAZON.emp =" & mTpa.emp.Id & " AND " _
        & "TPAZON.tpa =" & mTpa.Id & " AND " _
        & "RepZon.FchFrom < GETDATE() AND (RepZon.FchTo IS NULL or RepZon.FchTo > GETDATE()) " _
        & "GROUP BY RepZon.zon " _
        & "ORDER BY COUNT(DISTINCT REPZON.REP) DESC"

        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        If oDrd.Read Then
            If oDrd("REPES") > 1 Then
                TabPageReps.ImageKey = "WARN"
            Else
                TabPageReps.ImageKey = ""
            End If
        Else
        End If
        oDrd.Close()
    End Sub

    Private Sub LoadGridReps()
        Dim oProduct As DTOProduct = BLL.BLLProduct.Find(mTpa.Guid)

        Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(oProduct, True)
        Xl_RepProductsxRep1.Load(oRepProducts, Xl_RepProducts.Modes.ByProduct)
    End Sub


    Private Sub ButtonMailTpaReps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMailTpaReps.Click
        Dim oProduct As New Product(mTpa)
        Dim oReps As Reps = RepsLoader.FromProduct(oProduct)

        Dim s As String = ""
        For Each oRep As Rep In oReps
            s = s & oRep.Email & ";"
        Next
        Clipboard.SetDataObject(s, True)
        MsgBox("adreçes copiades a portapapers", MsgBoxStyle.Information, "MAT.NET")
        Exit Sub

    End Sub

    Private Sub ButtonRepsExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRepsExcel.Click
        Dim SQL As String = "SELECT G.NOM, G.ADR, G.ZIP, G.CIT,G.PROVINCIA, T.MOVIL1 " _
        & "FROM RepZon R INNER JOIN Cli_Geo3 G ON R.EMP=G.EMP AND R.REP=G.CLI LEFT OUTER JOIN " _
        & "CLI_MOVIL T ON G.EMP=T.EMP AND G.CLI=T.CLI " _
        & "WHERE R.EMP=@EMP AND R.TPA=@TPA AND R.FchFrom < GETDATE() AND (R.FchTo=NULL or R.FchTo > GETDATE()) " _
        & "GROUP BY G.NOM, G.ADR, G.ZIP, G.CIT,G.PROVINCIA, T.MOVIL1 " _
        & "ORDER BY G.NOM"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mTpa.emp.Id, "@TPA", mTpa.Id)
        MatExcel.GetExcelFromDataset(oDs).Visible = True
    End Sub
#End Region

#Region "Stps"
    Private mDefaultStp As Stp
    Private mInclouObsolets As Boolean
    Private mDsStps As DataSet

    Private Enum Cols
        Id
        Ico
        Nom
        Obsoleto
    End Enum

    Private Sub LoadStps()
        Dim SQL As String = "SELECT STP, DSC, OBSOLETO FROM Stp WHERE " _
        & "EMP=" & mTpa.emp.Id & " and " _
        & "TPA=" & mTpa.Id & " "
        If Not mInclouObsolets Then
            SQL = SQL & "AND OBSOLETO=0 "
        End If
        SQL = SQL & "ORDER BY OBSOLETO,ORD,STP"

        mDsStps = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsStps.Tables(0)

        'crea columna icono imatge
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        With DataGridViewStps
            With .RowTemplate
                .Height = DataGridViewStps.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DataSource = oTb
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .BackgroundColor = Color.White

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Obsoleto)
                .Visible = False
            End With
        End With
    End Sub


    Private Function CurrentStp() As Stp
        Dim oStp As Stp = Nothing
        Dim oRow As DataGridViewRow = DataGridViewStps.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = DataGridViewStps.CurrentRow.Cells(Cols.Id).Value
            oStp = New Stp(mTpa, LngId)
        End If
        Return oStp
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        Dim oGrid As DataGridView = DataGridViewStps

        If oGrid.Rows.Count > 0 Then
            Dim iRowIndex As Integer = DataGridViewStps.CurrentRow.Index
            Dim oStp As Stp = CurrentStp()

            If oStp IsNot Nothing Then
                Dim oMenu_Stp As New Menu_ProductCategory(oStp)
                AddHandler oMenu_Stp.AfterUpdate, AddressOf RefreshRequestStps
                oContextMenu.Items.AddRange(oMenu_Stp.Range)

                oMenuItem = New ToolStripMenuItem
                With oMenuItem
                    .Text = "predeterminada"
                    If mDefaultStp IsNot Nothing Then
                        .Checked = (mDefaultStp.Id = oStp.Id)
                    End If
                    .Enabled = Not .Checked
                    .CheckOnClick = True
                End With
                AddHandler oMenuItem.Click, AddressOf MenuItemChangeDefault
                oContextMenu.Items.Add(oMenuItem)

                oMenuItem = New ToolStripMenuItem
                With oMenuItem
                    .Text = "puja"
                    .Image = My.Resources.GoUp
                    .Enabled = (iRowIndex > 0)
                End With
                AddHandler oMenuItem.Click, AddressOf MenuItemGoUp
                oContextMenu.Items.Add(oMenuItem)

                oMenuItem = New ToolStripMenuItem
                With oMenuItem
                    .Text = "baixa"
                    .Image = My.Resources.GoDown
                    .Enabled = (iRowIndex < DataGridViewStps.Rows.Count - 1)
                End With
                AddHandler oMenuItem.Click, AddressOf MenuItemGoDown
                oContextMenu.Items.Add(oMenuItem)
            End If

            oContextMenu.Items.Add("-")
        End If


        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "refresca"
            .Image = My.Resources.refresca
        End With
        AddHandler oMenuItem.Click, AddressOf RefreshRequestStps
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "nova Categoria"
            .Image = My.Resources.NewDoc
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemAddStp
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "inclou obsolets"
            .Checked = mInclouObsolets
            .CheckOnClick = True
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemChangeObsolets
        oContextMenu.Items.Add(oMenuItem)

        DataGridViewStps.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub MenuItemChangeObsolets(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        mInclouObsolets = Not mInclouObsolets
        oMenuItem.Checked = mInclouObsolets
        RefreshRequestStps(sender, e)
    End Sub

    Private Sub MenuItemChangeDefault(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        mDefaultStp = CurrentStp()
        SetDirty()
        RefreshRequestStps(sender, e)
    End Sub

    Private Sub MenuItemAddStp(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oStp As New Stp(mTpa)
        ShowStp(oStp)
    End Sub

    Private Sub MenuItemGoUp(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim CurrentIdx As Integer = DataGridViewStps.CurrentRow.Index
        Dim NewCurrentIdx As Integer = CurrentIdx - 1
        If CurrentIdx >= 1 Then
            Switch(NewCurrentIdx, CurrentIdx)
            DataGridViewStps.CurrentCell = DataGridViewStps.Rows(NewCurrentIdx).Cells(Cols.Nom)
            RefreshRequestStps(sender, e)
        End If
    End Sub

    Private Sub MenuItemGoDown(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim CurrentIdx As Integer = DataGridViewStps.CurrentRow.Index
        Dim NewCurrentIdx As Integer = CurrentIdx + 1
        If CurrentIdx < DataGridViewStps.Rows.Count - 1 Then
            Switch(NewCurrentIdx, CurrentIdx)
            DataGridViewStps.CurrentCell = DataGridViewStps.Rows(NewCurrentIdx).Cells(Cols.Nom)
            RefreshRequestStps(sender, e)
        End If
    End Sub


    Private Sub Switch(ByVal i As Integer, ByVal j As Integer)
        Dim Idx As Integer
        Dim SQL As String
        For k As Integer = 0 To DataGridViewStps.Rows.Count - 1
            Select Case k
                Case i
                    Idx = j
                Case j
                    Idx = i
                Case Else
                    Idx = k
            End Select
            Dim StpId As Integer = DataGridViewStps.Rows(k).Cells(Cols.Id).Value
            SQL = "UPDATE Stp SET ORD=" & Idx & " WHERE " _
            & "EMP=" & mTpa.emp.Id & " AND " _
            & "TPA=" & mTpa.Id & " AND " _
            & "STP=" & StpId
            maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)
        Next
    End Sub

    Private Sub RefreshRequestStps(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridViewStps

        If oGrid.Rows.Count > 0 Then
            If oGrid.CurrentRow IsNot Nothing Then
                i = oGrid.CurrentRow.Index
                j = oGrid.CurrentCell.ColumnIndex
                iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
            End If
        End If

        LoadStps()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
        SetContextMenu()
    End Sub

    Private Sub DataGridViewStps_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewStps.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridViewStps.Rows(e.RowIndex)
                Dim BlObsoleto As Boolean = oRow.Cells(Cols.Obsoleto).Value
                If BlObsoleto Then
                    e.Value = My.Resources.del
                Else
                    Dim IsDefault As Boolean = False
                    If mDefaultStp IsNot Nothing Then
                        Dim CatId As Integer = oRow.Cells(Cols.Id).Value
                        IsDefault = (CatId = mDefaultStp.Id)
                    End If
                    If IsDefault Then
                        e.Value = My.Resources.star
                    Else
                        e.Value = My.Resources.empty
                    End If
                End If
        End Select

    End Sub

    Private Sub DataGridViewStp_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewStps.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridViewStps.Rows(e.RowIndex)
        Dim BlObsoleto As Boolean = oRow.Cells(Cols.Obsoleto).Value
        If BlObsoleto Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            If Not mDefaultStp Is Nothing Then
                Dim CatId As Integer = oRow.Cells(Cols.Id).Value
                If CatId = mDefaultStp.Id Then
                    PaintGradientRowBackGround(DataGridViewStps, e, Color.LightSteelBlue)
                Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
                End If
            Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal oGrid As DataGridView, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            oGrid.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            oGrid.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewStps.SelectionChanged
        SetContextMenu()
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewStps.DoubleClick
        ShowStp(CurrentStp())
    End Sub

    Private Sub ShowStp(ByVal oStp As Stp)
        Dim oFrm As New Frm_Stp(oStp, BLL.Defaults.SelectionModes.Browse)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestStps
        oFrm.Show()
    End Sub
#End Region

    Private Sub Xl_BigFileLogoVectorial_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_BigFileLogoVectorial.AfterUpdate
        mDirtyLogoVectorial = True
        SetDirty()
    End Sub


    Private Sub Xl_CodiMercancia1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_CodiMercancia1.AfterUpdate, _
     RadioButtonCodStkExtern.CheckedChanged, _
      RadioButtonCodStkIntern.CheckedChanged
        SetDirty()
    End Sub



    Private Sub RadioButtonMgz_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    RadioButtonMgzDefault.CheckedChanged, _
     RadioButtonMgzEspecific.CheckedChanged
        If mAllowEvents Then

            If RadioButtonMgzDefault.Checked Then
                Xl_Mgzs_ComboBox1.Mgz = Mgz.DefaultMgz
                Xl_Mgzs_ComboBox1.Enabled = False
            Else
                Xl_Mgzs_ComboBox1.Enabled = True
            End If
        End If
        SetDirty()
    End Sub


    Private Sub Xl_ImageEBook_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ImageEBook.AfterUpdate
        If mAllowEvents Then
            mDirtyEBook = True
            SetDirty()
        End If
    End Sub


    Private Sub Xl_TpaColeccions1_AfterSelect(sender As Object, e As System.EventArgs) Handles Xl_TpaColeccions1.AfterSelect
        Dim oColeccion As Coleccion = Xl_TpaColeccions1.Coleccion
        RaiseEvent AfterSelect(oColeccion, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub DataGridViewZonas_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)

    End Sub

    Private Sub CheckBoxWebPortadaFrom_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxWebPortadaFrom.CheckedChanged
        If mAllowEvents Then
            If CheckBoxWebPortadaFrom.Checked Then
                Dim DtFch As Date = mTpa.WebPortadaFrom
                If DtFch = Date.MinValue Then DtFch = Today

                DateTimePickerWebPortadaFrom.Visible = True
                DateTimePickerWebPortadaFrom.Value = DtFch
            Else
                DateTimePickerWebPortadaFrom.Visible = False
                DateTimePickerWebPortadaTo.Visible = False
                CheckBoxWebPortadaTo.Checked = False
            End If
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxWebPortadaTo_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxWebPortadaTo.CheckedChanged
        If mAllowEvents Then
            If CheckBoxWebPortadaTo.Checked Then
                Dim DtFch As Date = mTpa.WebPortadaTo
                If DtFch = Date.MinValue Then DtFch = Today

                DateTimePickerWebPortadaTo.Visible = True
                DateTimePickerWebPortadaTo.Value = DtFch
            Else
                DateTimePickerWebPortadaTo.Visible = False
            End If
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxWebEnabledConsumer_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxWebEnabledConsumer.CheckedChanged
        If mAllowEvents Then
            GroupBoxWebEnabledConsumer.Enabled = CheckBoxWebEnabledConsumer.Checked
            SetDirty()
        End If
    End Sub

    Private Sub Xl_ProductDownloads1_onFileDropped(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.onFileDropped
        Dim oProduct As New DTOProductSku(mTpa.Guid)
        Dim oDocFile As DTODocFile = e.Argument
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Product = oProduct
        oProductDownload.DocFile = oDocFile

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim oProduct As New DTOProductSku(mTpa.Guid)
        Dim oProductDownload As New DTOProductDownload
        oProductDownload.Product = oProduct

        Dim oFrm As New Frm_ProductDownload(oProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        LoadProductDownloads()
    End Sub

    Private Sub LoadProductDownloads()
        Dim oProduct As New DTOProductSku(mTpa.Guid)
        Dim oDownloads As List(Of DTOProductDownload) = BLL.BLLProductDownloads.FromProductOrParent(oProduct)
        Xl_ProductDownloads1.Load(oDownloads)
    End Sub

    Private Sub Xl_RepProductsxRep1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepProductsxRep1.RequestToAddNew
        Dim oProduct As DTOProduct = BLL.BLLProduct.Find(mTpa.Guid)
        Dim oRepProduct As DTORepProduct = BLL.BLLRepProduct.NewRepProduct(oProduct)
        Dim oRepProducts As New List(Of DTORepProduct)
        Dim oFrm As New Frm_RepProduct(oRepProducts)
        AddHandler oFrm.AfterUpdate, AddressOf LoadGridReps
        oFrm.Show()
    End Sub

    Private Sub Xl_RepProductsxRep1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepProductsxRep1.RequestToRefresh
        LoadGridReps()
    End Sub

    Private Sub CheckBoxOnlyEmpty_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOnlyEmpty.CheckedChanged
        If mAllowEvents Then
            LoadGridReps()
        End If
    End Sub

    Private Sub LoadGridZonas()
        Dim oBrand As DTOProductBrand = mTpa.Brand
        Dim oBrandAreas As List(Of DTOBrandArea) = BLL.BLLBrandAreas.All(oBrand)
        Xl_BrandAreas1.Load(oBrandAreas)
    End Sub

    Private Sub Xl_BrandAreas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BrandAreas1.RequestToAddNew
        Dim oBrand As DTOProductBrand = mTpa.Brand
        Dim oBrandArea As DTOBrandArea = BLL.BLLBrandArea.NewFromBrand(oBrand)
        Dim oFrm As New frm_BrandArea(oBrandArea)
        AddHandler oFrm.afterupdate, AddressOf LoadGridZonas
        oFrm.show()
    End Sub


    Private Sub Xl_BrandAreas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BrandAreas1.RequestToRefresh
        LoadGridZonas()
    End Sub

    Private Sub ButtonCancel_Click1(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class

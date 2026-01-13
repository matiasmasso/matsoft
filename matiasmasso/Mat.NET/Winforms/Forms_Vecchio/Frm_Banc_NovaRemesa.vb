
Imports System.Data.SqlClient

Public Class Frm_Banc_NovaRemesa
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ContextMenuCsbs As System.Windows.Forms.ContextMenu
    Friend WithEvents CheckedListBoxCsbs As System.Windows.Forms.CheckedListBox
    Friend WithEvents ComboBoxPais As System.Windows.Forms.ComboBox
    Friend WithEvents LabelPais As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents LabelCsaFormat As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ListBoxVtos As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ContextMenuCsbs = New System.Windows.Forms.ContextMenu()
        Me.CheckedListBoxCsbs = New System.Windows.Forms.CheckedListBox()
        Me.ListBoxVtos = New System.Windows.Forms.ListBox()
        Me.ComboBoxPais = New System.Windows.Forms.ComboBox()
        Me.LabelPais = New System.Windows.Forms.Label()
        Me.LabelCsaFormat = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Xl_Contact_Logo1 = New Xl_Contact_Logo()
        Me.SuspendLayout()
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(520, 361)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(112, 24)
        Me.ButtonOk.TabIndex = 2
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ContextMenuCsbs
        '
        '
        'CheckedListBoxCsbs
        '
        Me.CheckedListBoxCsbs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxCsbs.ContextMenu = Me.ContextMenuCsbs
        Me.CheckedListBoxCsbs.FormattingEnabled = True
        Me.CheckedListBoxCsbs.Location = New System.Drawing.Point(167, 66)
        Me.CheckedListBoxCsbs.Name = "CheckedListBoxCsbs"
        Me.CheckedListBoxCsbs.Size = New System.Drawing.Size(465, 289)
        Me.CheckedListBoxCsbs.TabIndex = 8
        '
        'ListBoxVtos
        '
        Me.ListBoxVtos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBoxVtos.FormattingEnabled = True
        Me.ListBoxVtos.Location = New System.Drawing.Point(10, 65)
        Me.ListBoxVtos.Name = "ListBoxVtos"
        Me.ListBoxVtos.Size = New System.Drawing.Size(150, 290)
        Me.ListBoxVtos.TabIndex = 9
        '
        'ComboBoxPais
        '
        Me.ComboBoxPais.FormattingEnabled = True
        Me.ComboBoxPais.Location = New System.Drawing.Point(568, 9)
        Me.ComboBoxPais.Name = "ComboBoxPais"
        Me.ComboBoxPais.Size = New System.Drawing.Size(64, 21)
        Me.ComboBoxPais.TabIndex = 10
        '
        'LabelPais
        '
        Me.LabelPais.AutoSize = True
        Me.LabelPais.Location = New System.Drawing.Point(533, 12)
        Me.LabelPais.Name = "LabelPais"
        Me.LabelPais.Size = New System.Drawing.Size(29, 13)
        Me.LabelPais.TabIndex = 11
        Me.LabelPais.Text = "pais:"
        '
        'LabelCsaFormat
        '
        Me.LabelCsaFormat.AutoSize = True
        Me.LabelCsaFormat.Location = New System.Drawing.Point(167, 13)
        Me.LabelCsaFormat.Name = "LabelCsaFormat"
        Me.LabelCsaFormat.Size = New System.Drawing.Size(83, 13)
        Me.LabelCsaFormat.TabIndex = 13
        Me.LabelCsaFormat.Text = "LabelCsaFormat"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(10, 361)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(504, 23)
        Me.ProgressBar1.TabIndex = 14
        Me.ProgressBar1.Visible = False
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(10, 9)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 12
        '
        'Frm_Banc_NovaRemesa
        '
        Me.ClientSize = New System.Drawing.Size(640, 390)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.LabelCsaFormat)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Controls.Add(Me.LabelPais)
        Me.Controls.Add(Me.ComboBoxPais)
        Me.Controls.Add(Me.ListBoxVtos)
        Me.Controls.Add(Me.CheckedListBoxCsbs)
        Me.Controls.Add(Me.ButtonOk)
        Me.Name = "Frm_Banc_NovaRemesa"
        Me.Text = "REMESA DE EFECTES AL COBRO"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _Csa As DTOCsa
    Private _Pnds As List(Of DTOPnd)
    Private mVtosArray As ArrayList
    Private mVto As Date

    Private mAllowEvents As Boolean

    Public Sub New(ByVal oCsa As DTOCsa)
        MyBase.New()
        Me.InitializeComponent()

        _Csa = oCsa
        Xl_Contact_Logo1.Load(_Csa.Banc)
        LabelCsaFormat.Text = _Csa.FileFormat.ToString

        Dim sTit As String
        Select Case _Csa.FileFormat
            Case DTOCsa.FileFormats.Norma19, DTOCsa.FileFormats.SepaCore
                sTit = "REMESA DE EFECTES AL COBRO (SEPA CORE)"
            Case DTOCsa.FileFormats.Norma58
                sTit = "REMESA DE EFECTES AL COBRO (Norma 58)"
            Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                sTit = "REMESA MASIVA DE EXPORTACIO"
            Case DTOCsa.FileFormats.NormaAndorrana
                sTit = "REMESA DE EFECTES AL COBRO (Norma Andorrana)"
            Case Else
                sTit = "REMESA DE EFECTES AL COBRO"
        End Select
        Me.Text = sTit & " " & BLL.BLLIban.BankNom(_Csa.Banc.Iban)

        Select Case _Csa.FileFormat
            Case DTOCsa.FileFormats.Norma19, DTOCsa.FileFormats.Norma58, DTOCsa.FileFormats.SepaCore
                ComboBoxPais.Items.Add("ES")
            Case DTOCsa.FileFormats.NormaAndorrana
                ComboBoxPais.Items.Add("AD")
            Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                LoadPaisos()
            Case DTOCsa.FileFormats.SepaB2b
                LabelPais.Visible = False
                ComboBoxPais.Visible = False
        End Select

        If ComboBoxPais.Items.Count > 0 Then ComboBoxPais.SelectedIndex = 0

        LoadVtosDeUnPais()
        mAllowEvents = True
    End Sub

    Private Function CurrentCountry() As DTOCountry
        Dim oCountry As DTOCountry = BLLCountry.DefaultCountry
        If ComboBoxPais.SelectedIndex >= 0 Then
            Dim sISOPais As String = ComboBoxPais.SelectedItem
            oCountry = BLLCountry.Find(sISOPais)
        End If
        Return oCountry
    End Function

    Private Sub LoadPaisos()
        Dim oCountries As List(Of DTOCountry) = BLLPnds.SepaCorePendingCountries
        With ComboBoxPais
            .DisplayMember = "ISO"
            .DataSource = oCountries
        End With
    End Sub
    Private Sub LoadPaisos_Old()
        Dim SQL As String = "SELECT SUBSTRING(IBAN.CCC, 1, 2) AS PAIS " _
        & "FROM            PND INNER JOIN " _
        & "IBAN ON PND.ContactGuid = IBAN.ContactGuid " _
        & "WHERE        (PND.ad LIKE 'D') AND (PND.eur > 0) AND (PND.Status = 0) AND (PND.cfp = 5) " _
        & "GROUP BY SUBSTRING(IBAN.CCC, 1, 2) " _
        & "ORDER BY PAIS"

        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim sPais As String = oDrd("PAIS").ToString
            If sPais <> "ES" Then ComboBoxPais.Items.Add(sPais)
        Loop
        oDrd.Close()
    End Sub

    Private Sub LoadVtosDeUnPais()
        Dim SQL As String = "SELECT vto, SUM(eur) AS EUR " _
        & "FROM PND INNER JOIN " _
        & "IBAN ON PND.ContactGuid = IBAN.ContactGuid " _
        & "WHERE        (PND.ad LIKE 'D') AND (PND.eur > 0) AND (PND.Status = 0) AND (PND.cfp = 5) "

        Select Case _Csa.FileFormat
            Case DTOCsa.FileFormats.SepaB2b
                SQL = SQL & "AND IBAN.FORMAT=" & CInt(DTOIban.Formats.SEPAB2B) & " "
                SQL = SQL & "AND IBAN.HASH IS NOT NULL "
            Case Else
                SQL = SQL & "AND SUBSTRING(IBAN.CCC, 1, 2) = '" & CurrentCountry.ISO & "' "
        End Select

        SQL = SQL & "AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) "
        SQL = SQL & "GROUP BY vto " _
        & "ORDER BY vto"

        mVtosArray = New ArrayList
        ListBoxVtos.Items.Clear()
        Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            mVtosArray.Add(oDrd("VTO"))
            ListBoxVtos.Items.Add(VtoText(oDrd("VTO"), oDrd("EUR")))
        Loop
    End Sub

    Private Function VtoText(ByVal Vto As Date, ByVal Eur As Decimal) As String
        Dim sVto As String = Format(Vto, "dd/MM/yy")
        Dim sEur As String = Format(Eur, "#,###.00")
        Dim sTmp As String = ""
        Dim Len As Integer = sEur.Length
        Dim PadSpaces As Integer = 2 * (10 - Len)
        If Len < 7 Then PadSpaces = PadSpaces - 1 '7 ES EL PUNT

        Return sVto & sTmp.PadLeft(PadSpaces) & sEur
    End Function

    Private Function ItmVto(ByVal ItmText As String) As Date
        Return ItmText.Substring(0, 8)
    End Function


    Private Sub LoadCsbs()
        Cursor = Cursors.WaitCursor
        Dim SQL As String = "SELECT Pnd.Guid, PND.ID, PND.eur, CLX.clx, PND.ContactGuid, Mandato_Guid, IBAN.Format  " _
               & ", Iban.Ccc " _
               & "FROM PND INNER JOIN " _
               & "CLX ON PND.ContactGuid = Clx.Guid INNER JOIN " _
               & "CLIADR ON PND.ContactGuid=CLIADR.SrcGuid AND CLIADR.COD=1 INNER JOIN " _
               & "IBAN ON PND.ContactGuid = IBAN.ContactGuid AND IBAN.Cod=" & CInt(DTOIban.Cods.Client) & " "

        Select Case _Csa.FileFormat
            Case DTOCsa.FileFormats.SepaB2b
                SQL = SQL & "AND IBAN.FORMAT=" & CInt(DTOIban.Formats.SEPAB2B) & " "
                SQL = SQL & "AND IBAN.HASH IS NOT NULL "
            Case Else
                SQL = SQL & "AND SUBSTRING(IBAN.CCC, 1, 2) = @Pais "
        End Select

        SQL = SQL & "AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) " _
               & "WHERE PND.Emp =" & BLLApp.Emp.Id & " AND " _
               & "cfp = 5 AND " _
               & "ad LIKE 'D' AND " _
               & "PND.eur > 0 AND " _
               & "PND.STATUS=" & Pnd.StatusCod.pendent & " AND " _
               & "PND.vto = '" & Format(mVto, "yyyyMMdd") & "' " _
               & "ORDER BY CLX.clx"

        CheckedListBoxCsbs.Items.Clear()
        Dim itm As System.Windows.Forms.ListViewItem
        Dim oPnd As DTOPnd
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Pais", CurrentCountry.ISO)
        Dim oTb As DataTable = oDs.Tables(0)
        With ProgressBar1
            .Visible = True
            .Minimum = 0
            .Value = 0
            .Maximum = oTb.Rows.Count
        End With
        Application.DoEvents()

        Dim StNegatius As String = ""
        _Pnds = New List(Of DTOPnd)
        For Each oRow As DataRow In oTb.Rows
            Dim oContact As New DTOContact(CType(oRow("ContactGuid"), Guid))
            Dim oClient As New DTOCustomer(oContact.Guid)
            oClient.FullNom = oRow("Clx").ToString
            oPnd = New DTOPnd(oRow("Guid"))
            With oPnd
                .Id = oRow("Id")
                .Amt = BLLApp.GetAmt(CDec(oRow("Eur")))
                .Contact = oClient
                StNegatius = StNegatius & CheckNegatius(oClient)
            End With
            _Pnds.Add(oPnd)
            itm = New System.Windows.Forms.ListViewItem(oPnd.Amt.Formatted.ToString)
            'Dim idx As Integer = CheckedListBoxCsbs.Items.Add(PndText(oPnd), CheckState.Unchecked)


            CheckedListBoxCsbs.Items.Add(PndText(oPnd), CheckState.Checked)

            ProgressBar1.Increment(1)
            Application.DoEvents()
        Next
        If StNegatius > "" Then MsgBox("Negatius:" & vbCrLf & StNegatius, MsgBoxStyle.Exclamation)
        ProgressBar1.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Function CheckNegatius(ByVal oContact As DTOContact) As String

        Dim retval As String = ""
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid FROM Pnd ")
        sb.AppendLine("WHERE ContactGuid='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Eur <0 ")
        sb.AppendLine("AND Status =" & CInt(Pnd.StatusCod.pendent) & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oContact.FullNom
        End If
        oDrd.Close()
        Return retval
    End Function

    Private Function PndText(ByVal oPnd As DTOPnd) As String
        Dim str As String
        Dim oClient As New Client(oPnd.Contact.Guid)
        str = oPnd.Amt.Formatted & " " & oPnd.Contact.FullNom
        'Dim oIban As DTOIban = oClient.FormaDePago.Iban
        'If oIban Is Nothing Then
        ' str = str & " (falta domiciliació)"
        'Else
        'str = str & " " & BLL.BLLIban.Formated(oIban)
        'End If
        Return str
    End Function



    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oPnd As DTOPnd

        With ProgressBar1
            .Visible = True
            .Minimum = 0
            .Value = 0
            .Maximum = CheckedListBoxCsbs.Items.Count
        End With
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        With _Csa
            .Fch = Today
            Dim i As Integer
            For i = 0 To CheckedListBoxCsbs.Items.Count - 1
                If CheckedListBoxCsbs.GetItemCheckState(i) = CheckState.Checked Then
                    oPnd = _Pnds(i)

                    Dim oIban As DTOIban = BLLIban.FromContact(oPnd.Contact, DTOIban.Cods.Client)
                    Dim oCsb As New DTOCsb()
                    With oCsb
                        .Contact = oPnd.Contact
                        .Amt = oPnd.Amt
                        .Vto = mVto
                        .Invoice = oPnd.Invoice
                        .Txt = String.Format("fra.{0} del {1:dd/MM/yy}", .Invoice.Num, .Invoice.Fch)
                        .Iban = oIban
                        .Pnd = oPnd
                    End With
                    .Items.Add(oCsb)
                End If
                ProgressBar1.Increment(1)
                Application.DoEvents()
            Next

            Dim exs As New List(Of Exception)
            Dim oCsa As New DTOCsa(_Csa.Guid)
            If BLLCsa.SaveRemesaCobrament(oCsa, BLLSession.Current.User, exs) Then
                Dim FileSrc As String = BLLSepaCore.XML(oCsa)
                UIHelper.SaveXmlFileDialog(FileSrc)
                Me.Close()
            Else
                Cursor = Cursors.Default
                UIHelper.WarnError(exs)
                ProgressBar1.Visible = False
            End If

        End With

    End Sub

    Private Sub ListBoxVtos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxVtos.SelectedIndexChanged
        mVto = ItmVto(ListBoxVtos.SelectedItem)
        LoadCsbs()
    End Sub

    Private Sub MenuItemCsbsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadCsbs()
    End Sub

    Private Function CurrentPnd() As DTOPnd
        Dim oPnd As DTOPnd = Nothing
        Dim Idx As Integer = CheckedListBoxCsbs.SelectedIndex
        If Idx >= 0 Then
            oPnd = _Pnds(Idx)
        End If
        Return oPnd
    End Function



    Private Sub ComboBoxPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPais.SelectedIndexChanged
        If mAllowEvents Then
            LoadVtosDeUnPais()
        End If
    End Sub

    Private Sub CheckedListBoxCsbs_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxCsbs.ItemCheck
        Dim BlEnabled As Boolean = True
        If e.CurrentValue = CheckState.Indeterminate Then
            e.NewValue = CheckState.Indeterminate
        Else
            If e.NewValue = CheckState.Unchecked Then
                If CheckedListBoxCsbs.CheckedItems.Count <= 1 Then
                    BlEnabled = False
                End If
            End If
            ButtonOk.Enabled = BlEnabled
        End If
    End Sub
End Class

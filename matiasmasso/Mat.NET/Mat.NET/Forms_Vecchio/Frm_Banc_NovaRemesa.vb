
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

    Private mCsa As Csa
    Private mPnds As MaxiSrvr.Pnds
    Private mVtosArray As ArrayList
    Private mVto As Date

    Private mAllowEvents As Boolean

    Public Sub New(ByVal oCsa As Csa)
        MyBase.New()
        Me.InitializeComponent()

        mCsa = oCsa
        Xl_Contact_Logo1.Contact = mCsa.Banc
        LabelCsaFormat.Text = mCsa.FileFormat.ToString

        Dim sTit As String
        Select Case mCsa.FileFormat
            Case DTOCsa.FileFormats.Norma19
                sTit = "REMESA DE EFECTES AL COBRO (Norma 19)"
            Case DTOCsa.FileFormats.Norma58
                sTit = "REMESA DE EFECTES AL COBRO (Norma 58)"
            Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                sTit = "REMESA MASIVA DE EXPORTACIO"
            Case DTOCsa.FileFormats.NormaAndorrana
                sTit = "REMESA DE EFECTES AL COBRO (Norma Andorrana)"
            Case Else
                sTit = "REMESA DE EFECTES AL COBRO"
        End Select
        Me.Text = sTit & " " & BLL.BLLIban.BankNom(mCsa.Banc.Iban)

        Select Case mCsa.FileFormat
            Case DTOCsa.FileFormats.Norma19, DTOCsa.FileFormats.Norma58
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

    Private Function CurrentCountry() As Country
        Dim oCountry As Country = Country.Default
        If ComboBoxPais.SelectedIndex >= 0 Then
            Dim sPais As String = ComboBoxPais.SelectedItem
            oCountry = New Country(sPais)
        End If
        Return oCountry
    End Function

    Private Sub LoadPaisos()
        Dim SQL As String = "SELECT SUBSTRING(IBAN.CCC, 1, 2) AS PAIS " _
        & "FROM            PND INNER JOIN " _
        & "IBAN ON PND.ContactGuid = IBAN.ContactGuid " _
        & "WHERE        (PND.ad LIKE 'D') AND (PND.eur > 0) AND (PND.Status = 0) AND (PND.cfp = 5) " _
        & "GROUP BY SUBSTRING(IBAN.CCC, 1, 2) " _
        & "ORDER BY PAIS"

        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
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

        Select Case mCsa.FileFormat
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
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
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
        Dim SQL As String = "SELECT PND.ID, PND.eur, CLX.clx, PND.ContactGuid, Mandato_Guid, IBAN.Format  " _
               & "FROM PND INNER JOIN " _
               & "CLX ON PND.ContactGuid = Clx.Guid INNER JOIN " _
               & "CLIADR ON PND.ContactGuid=CLIADR.SrcGuid AND CLIADR.COD=1 INNER JOIN " _
               & "IBAN ON PND.ContactGuid = IBAN.ContactGuid "

        Select Case mCsa.FileFormat
            Case DTOCsa.FileFormats.SepaB2b
                SQL = SQL & "AND IBAN.FORMAT=" & CInt(DTOIban.Formats.SEPAB2B) & " "
                SQL = SQL & "AND IBAN.HASH IS NOT NULL "
            Case Else
                SQL = SQL & "AND SUBSTRING(IBAN.CCC, 1, 2) = @Pais "
        End Select

        SQL = SQL & "AND (IBAN.Caduca_Fch IS NULL OR IBAN.Caduca_Fch > GETDATE()) " _
               & "WHERE PND.Emp =" & App.Current.Emp.Id & " AND " _
               & "cfp = 5 AND " _
               & "ad LIKE 'D' AND " _
               & "PND.eur > 0 AND " _
               & "PND.STATUS=" & Pnd.StatusCod.pendent & " AND " _
               & "PND.vto = '" & Format(mVto, "yyyyMMdd") & "' " _
               & "ORDER BY CLX.clx"

        CheckedListBoxCsbs.Items.Clear()
        Dim itm As Windows.Forms.ListViewItem
        Dim oPnd As New MaxiSrvr.Pnd
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Pais", CurrentCountry.ISO)
        Dim oTb As DataTable = oDs.Tables(0)
        With ProgressBar1
            .Visible = True
            .Minimum = 0
            .Value = 0
            .Maximum = oTb.Rows.Count
        End With
        Application.DoEvents()

        Dim StNegatius As String = ""
        mPnds = New MaxiSrvr.Pnds
        For Each oRow As DataRow In oTb.Rows
            Dim oContact As New Contact(CType(oRow("ContactGuid"), Guid))
            Dim oClient As New Client(oContact.Guid)
            oClient.Clx = oRow("Clx").ToString
            oPnd = New MaxiSrvr.Pnd(oRow("ID"))
            With oPnd
                .Amt = New Amt(CDec(oRow("Eur")))
                .Contact = oClient
                StNegatius = StNegatius & CheckNegatius(oClient)
            End With
            mPnds.Add(oPnd)
            itm = New Windows.Forms.ListViewItem(oPnd.Amt.Formatted.ToString)
            'Dim idx As Integer = CheckedListBoxCsbs.Items.Add(PndText(oPnd), CheckState.Unchecked)

            If mCsa.FileFormat = DTOCsa.FileFormats.SepaB2b Then
                Dim oBank As DTOBank = BLL.BLLIban.Bank(oClient.FormaDePago.Iban)
                Dim BlSepa As Boolean = oBank.SEPAB2B
                If IsDBNull(oRow("Mandato_Guid")) Then BlSepa = False
                If BlSepa Then
                    CheckedListBoxCsbs.Items.Add(PndText(oPnd), CheckState.Checked)
                Else
                    CheckedListBoxCsbs.Items.Add(PndText(oPnd), CheckState.Indeterminate)
                End If
            Else
                CheckedListBoxCsbs.Items.Add(PndText(oPnd), CheckState.Checked)
            End If

            ProgressBar1.Increment(1)
            Application.DoEvents()
        Next
        If StNegatius > "" Then MsgBox("Negatius:" & vbCrLf & StNegatius, MsgBoxStyle.Exclamation)
        ProgressBar1.Visible = False
        Cursor = Cursors.Default

    End Sub

    Private Function CheckNegatius(ByVal oCli As MaxiSrvr.Client) As String

        Dim sRetVal As String = ""
        Dim SQL As String = "SELECT ID FROM PND WHERE EMP=@Emp AND " _
        & "CLI=@Cli AND " _
        & "EUR<0 AND " _
        & "STATUS=@Status"

        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@Emp", oCli.Emp.Id, "@Cli", oCli.Id, "@Status", CInt(Pnd.StatusCod.pendent).ToString)
        If oDrd.Read Then
            sRetVal = oCli.Clx & vbCrLf
        End If

        Dim oIban As DTOIban = oCli.Client.FormaDePago.Iban
        If oIban Is Nothing Then
            sRetVal = "Falta domiciliació: " & oCli.Clx & vbCrLf
        Else
            If oIban.Digits.Substring(4, 8) <= "00000000" Then
                sRetVal = "xerocopia: " & oCli.Clx & vbCrLf
            End If
        End If

        oDrd.Close()
        Return sRetVal
    End Function

    Private Function PndText(ByVal oPnd As MaxiSrvr.Pnd) As String
        Dim str As String
        Dim oClient As New Client(oPnd.Contact.Guid)
        str = oPnd.Amt.Formatted & " " & oClient.Clx
        Dim oIban As DTOIban = oClient.FormaDePago.Iban
        If oIban Is Nothing Then
            str = str & " (falta domiciliació)"
        Else
            str = str & " " & BLL.BLLIban.Formated(oIban)
        End If
        Return str
    End Function



    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oCsb As MaxiSrvr.Csb
        Dim oPnd As MaxiSrvr.Pnd

        With ProgressBar1
            .Visible = True
            .Minimum = 0
            .Value = 0
            .Maximum = CheckedListBoxCsbs.Items.Count
        End With
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        With mCsa
            .fch = Today
            Dim i As Integer
            For i = 0 To CheckedListBoxCsbs.Items.Count - 1
                If CheckedListBoxCsbs.GetItemCheckState(i) = CheckState.Checked Then
                    oPnd = mPnds(i)
                    oCsb = oPnd.GetNewCsb(mCsa.FileFormat = DTOCsa.FileFormats.SepaB2b, mCsa)
                    .csbs.Add(oCsb)
                End If
                ProgressBar1.Increment(1)
                Application.DoEvents()
            Next

            Dim exs as New List(Of exception)
            If .Update( exs) Then
                'If Not .descomptat Then
                .Despeses = .SetDespeses_AEB19(root.Usuari, exs)
                'End If
                Application.DoEvents()

                root.SaveCsaFile(mCsa)

                Me.Close()
            Else
                Cursor = Cursors.Default
                MsgBox("error al desar la remesa:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
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

    Private Function CurrentPnd() As Pnd
        Dim oPnd As Pnd = Nothing
        Dim Idx As Integer = CheckedListBoxCsbs.SelectedIndex
        If Idx >= 0 Then
            oPnd = mPnds(Idx)
        End If
        Return oPnd
    End Function

    Private Sub ContextMenuCsbs_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuCsbs.Popup
        'Dim oItm As MenuItem
        With ContextMenuCsbs.MenuItems
            .Clear()
            .Add("refresca", New System.EventHandler(AddressOf MenuItemCsbsRefresh_Click))
            .Add("-")

            Dim oPnd As Pnd = CurrentPnd()
            If oPnd IsNot Nothing Then
                'For Each oItm In New MenuItems_Contact(CurrentPnd.Contact)
                ' .Add(oItm.CloneMenu)
                ' Next
            End If
        End With
    End Sub

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

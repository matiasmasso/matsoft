Public Class Xl_BookFras
    Inherits DataGridView

    Private _Values As List(Of DTOBookFra)
    Private _DefaultValue As DTOBookFra
    Private _UniqueContact As DTOContact
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _filtraPending As Boolean
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Csv
        Fch
        Nom
        Nif
        Cta
        Fra
        Exenta
        ClaveExenta
        Sujeta
        IVA
        IRPF
        Liq
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBookFra), Optional oDefaultValue As DTOBookFra = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values
        _SelectionMode = oSelectionMode

        If _Values IsNot Nothing AndAlso _Values.Count > 0 Then
            Dim oFirstContact = _Values.First.Contact
            If _Values.All(Function(x) x.Contact.Equals(oFirstContact)) Then
                _UniqueContact = oFirstContact
            End If
        End If

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOBookFra)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False

        Dim iCurrentRow As Integer
        Dim iFirstDisplayedRow As Integer
        If MyBase.CurrentRow IsNot Nothing Then
            iCurrentRow = MyBase.CurrentRow.Index
            iFirstDisplayedRow = MyBase.FirstDisplayedScrollingRowIndex
        End If

        Dim oFilteredValues As List(Of DTOBookFra) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOBookFra In oFilteredValues
            'If oItem.Contact.Nom.Contains("Oihana") Then Stop
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)

        Next

        Dim DcSujetas As Decimal = _ControlItems.Sum(Function(x) x.Sujeta)
        Dim DcExentas As Decimal = _ControlItems.Sum(Function(x) x.Exenta)
        'Dim DcIvas As Decimal = _ControlItems.Sum(Function(x) x.IvaAmt)
        'Dim DcIrpfs As Decimal = _ControlItems.Sum(Function(x) x.IrpfAmt)
        Dim oTotals As New ControlItem(DcSujetas, DcExentas)
        _ControlItems.Insert(0, oTotals)

        MyBase.DataSource = _ControlItems

        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue Is Nothing Then
            If MyBase.Rows.Count > iCurrentRow Then
                MyBase.CurrentCell = MyBase.Rows(iCurrentRow).Cells(Cols.Fch)
            Else
                If MyBase.Rows.Count > 0 Then
                    MyBase.CurrentCell = MyBase.Rows(MyBase.Rows.Count - 1).Cells(Cols.Fch)
                End If
            End If

            If MyBase.Rows.Count > iFirstDisplayedRow Then
                MyBase.FirstDisplayedScrollingRowIndex = iFirstDisplayedRow
            Else
                If MyBase.Rows.Count > 0 Then
                    MyBase.FirstDisplayedScrollingRowIndex = MyBase.Rows.Count - 1
                End If
            End If
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If



        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOBookFra)
        Dim retval As List(Of DTOBookFra)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Contact.Nom.ToLower.Contains(_Filter.ToLower) Or x.FraNum.Contains(_Filter.ToLower))
        End If
        If _filtraPending Then
            retval = retval.FindAll(Function(x) x.SiiLog.Result <> DTOInvoice.SiiResults.Correcto)
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOBookFra
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBookFra = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Csv), DataGridViewImageColumn)
            .DataPropertyName = "Csv"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = _UniqueContact Is Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nif)
            .HeaderText = "NIF"
            .DataPropertyName = "Nif"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Visible = _UniqueContact Is Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cta)
            .HeaderText = "Compte"
            .DataPropertyName = "Cta"
            .Width = 120
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fra)
            .HeaderText = "Factura"
            .DataPropertyName = "Fra"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Exenta)
            .HeaderText = "Exempta"
            .DataPropertyName = "Exenta"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.ClaveExenta)
            .HeaderText = "Clau"
            .DataPropertyName = "ClaveExenta"
            .Width = 30
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sujeta)
            .HeaderText = "Subjecte"
            .DataPropertyName = "Sujeta"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.IVA)
            .HeaderText = "Iva"
            .DataPropertyName = "Iva"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.IRPF)
            .HeaderText = "Irpf"
            .DataPropertyName = "Irpf"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Liq)
            .HeaderText = "Liquid"
            .DataPropertyName = "Liq"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOBookFra)
        Dim retval As New List(Of DTOBookFra)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            If oControlItem.lin = ControlItem.lins.Standard Then
                Dim oMenu_BookFra As New Menu_BookFra(SelectedItems)
                AddHandler oMenu_BookFra.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_BookFra.Range)
            End If
        End If
        oContextMenu.Items.Add("sincronitza amb sii", Nothing, AddressOf Do_SyncPeriod)

        Dim oMenuItem As New ToolStripMenuItem("filtra pendents de sii", Nothing, AddressOf Do_pending)
        oMenuItem.CheckOnClick = True
        oMenuItem.Checked = _filtraPending
        oContextMenu.Items.Add(oMenuItem)
        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_pending()
        _filtraPending = Not _filtraPending
        Refresca()
    End Sub

    Private Async Sub Do_SyncPeriod()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oBookFra As DTOBookFra = oControlItem.Source
        Dim oExercici As DTOExercici = oBookFra.Cca.Exercici
        Dim DtFch As Date = oBookFra.Cca.Fch
        Dim iMes As Integer = DtFch.Month
        Dim exs As New List(Of Exception)
        Dim oX509Cert = Await FEB.Cert.X509Certificate2(GlobalVariables.Emp.Org, exs)
        If exs.Count = 0 Then
            Dim oBookfras As List(Of DTOBookFra) = FilteredValues.Where(Function(x) x.Cca.Exercici.Year = oExercici.Year And x.Cca.Fch.Month = iMes).ToList
            Dim oConsultats As List(Of DTOSiiConsulta) = FEB.Bookfras.Consulta(Current.Session.Emp, DTO.Defaults.Entornos.Produccion, oX509Cert, oExercici, iMes, exs)
            For Each oBookFra In oBookfras
                Dim oConsultat As DTOSiiConsulta = oConsultats.FirstOrDefault(Function(x) x.Nif = oBookFra.contact.PrimaryNifValue() And x.Invoice = oBookFra.fraNum)
                If oConsultat IsNot Nothing Then
                    If oBookFra.SiiLog Is Nothing Then
                        oBookFra.SiiLog = New DTOSiiLog
                        With oBookFra.SiiLog
                            .Contingut = DTOSiiLog.Continguts.Facturas_Recibidas
                            .Entorno = DTO.Defaults.Entornos.Produccion
                            .TipoDeComunicacion = "A0"
                            .Csv = oConsultat.Csv
                            .Fch = oConsultat.Fch
                        End With
                    End If
                    With oBookFra
                        .SiiLog.Result = oConsultat.EstadoRegistro
                        .SiiLog.ErrMsg = oConsultat.DescripcionErrorRegistro
                        '.SiiErrCod = oConsultat.CodigoErrorRegistro
                        '.SiiEstadoCuadre = oConsultat.EstadoCuadre
                        '.SiiTimestampEstadoCuadre = oConsultat.TimestampEstadoCuadre
                        '.SiiTimestampUltimaModificacion = oConsultat.TimestampUltimaModificacion
                    End With
                    If Not Await FEB.BookFra.Update(exs, oBookFra) Then
                        UIHelper.WarnError(exs)
                    End If
                End If
            Next
            Refresca()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBookFra = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    If oSelectedValue IsNot Nothing Then
                        Dim oFrm As New Frm_BookFra(oSelectedValue)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    End If
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_BookFras_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oBookFra As DTOBookFra = oControlItem.Source
                If oBookFra IsNot Nothing Then
                    If oBookFra.Cca.DocFile IsNot Nothing Then
                        e.Value = My.Resources.pdf
                    End If
                End If
            Case Cols.Csv
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oBookFra As DTOBookFra = oControlItem.Source
                If oBookFra IsNot Nothing Then
                    Select Case oBookFra.SiiLog.Result
                        Case DTOInvoice.SiiResults.Correcto
                            e.Value = My.Resources.sheriff
                        Case DTOInvoice.SiiResults.AceptadoConErrores
                            e.Value = My.Resources.warn
                        Case DTOInvoice.SiiResults.Incorrecto
                            e.Value = My.Resources.WarnRed16
                    End Select
                End If
            Case Cols.ClaveExenta
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.lin = ControlItem.lins.Standard And oControlItem.Exenta <> 0 And oControlItem.ClaveExenta = "" Then
                    e.CellStyle.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                End If
        End Select
    End Sub



    Protected Class ControlItem
        Public Property Source As DTOBookFra
        Property lin As lins
        Property ico As Image
        Property Fch As Nullable(Of Date)
        Property Nom As String
        Property Nif As String
        Property Cta As String
        Property Csv As String
        Property Fra As String
        Property Exenta As Decimal
        Property ClaveExenta As String
        Property Sujeta As Decimal
        Property Iva As Decimal
        Property Irpf As Decimal
        Property Liq As Decimal

        Public Enum lins
            Standard
            Totals
        End Enum

        Public Sub New(oBookFra As DTOBookFra)
            MyBase.New()
            _Source = oBookFra
            With oBookFra
                _lin = lins.Standard
                _Fch = .Cca.Fch
                If .Contact IsNot Nothing Then
                    _Nom = .Contact.Nom
                    _Nif = .contact.PrimaryNifValue()
                End If
                If .Contact IsNot Nothing Then
                    _Cta = DTOPgcCta.FullNom(.Cta, Current.Session.Lang)
                End If
                _Fra = .FraNum
                If .BaseQuotaIvaExenta IsNot Nothing Then
                    If .baseQuotaIvaExenta.baseImponible IsNot Nothing Then
                        _Exenta = .baseQuotaIvaExenta.baseImponible.eur
                    End If
                End If
                _ClaveExenta = .ClaveExenta

                If .BaseQuotaIvaSoportat IsNot Nothing Then
                    If .baseQuotaIvaSoportat.baseImponible IsNot Nothing Then
                        _Sujeta = .baseQuotaIvaSoportat.baseImponible.eur
                    End If
                    _Iva = .BaseQuotaIvaSoportat.Tipus
                End If

                If .IrpfBaseQuota IsNot Nothing Then
                    _Irpf = .IrpfBaseQuota.Tipus
                End If

                _Liq = .Total.Eur
            End With
        End Sub

        Public Sub New(DcSujetas As Decimal, DcExentas As Decimal)
            MyBase.New()
            _lin = lins.Totals
            _Nom = "totals"
            _Exenta = DcExentas
            _Sujeta = DcSujetas
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class



End Class


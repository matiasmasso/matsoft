Public Class Frm_PriceLists_Compare
    Private _PriceLists_Supplier As List(Of DTOPriceList_Supplier)
    Private _PriceLists_Customer As List(Of DTOPricelistCustomer)
    Private _Mode As Modes
    Private _AllowEvents As Boolean

    Private Enum Modes
        Supplier
        Customer
    End Enum

    Private Enum Cols
        ArtId
        ArtNom
        TarifaX
        TarifaY
        Diff
    End Enum

    Public Sub New(oPriceLists_Supplier As List(Of DTOPriceList_Supplier))
        MyBase.New()
        Me.InitializeComponent()
        _Mode = Modes.Supplier
        If oPriceLists_Supplier.Count >= 2 Then
            _PriceLists_Supplier = New List(Of DTOPriceList_Supplier)
            If oPriceLists_Supplier(1).Fch > oPriceLists_Supplier(0).Fch Then
                _PriceLists_Supplier.Add(oPriceLists_Supplier(0))
                _PriceLists_Supplier.Add(oPriceLists_Supplier(1))
            Else
                _PriceLists_Supplier.Add(oPriceLists_Supplier(1))
                _PriceLists_Supplier.Add(oPriceLists_Supplier(0))
            End If
        End If
    End Sub

    Public Sub New(oPriceLists_Customer As List(Of DTOPricelistCustomer))
        MyBase.New()
        Me.InitializeComponent()
        _Mode = Modes.Customer
        If oPriceLists_Customer.Count >= 2 Then
            _PriceLists_Customer = New List(Of DTOPricelistCustomer)
            If oPriceLists_Customer(1).Fch > oPriceLists_Customer(0).Fch Then
                _PriceLists_Customer.Add(oPriceLists_Customer(0))
                _PriceLists_Customer.Add(oPriceLists_Customer(1))
            Else
                _PriceLists_Customer.Add(oPriceLists_Customer(1))
                _PriceLists_Customer.Add(oPriceLists_Customer(0))
            End If
        End If
    End Sub

    Private Sub Frm_PriceLists_Compare_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iCount As Integer
        Select Case _Mode
            Case Modes.Supplier
                iCount= _PriceLists_Supplier.Count
            Case Modes.Customer
                iCount = _PriceLists_Customer.Count
        End Select

        If iCount >= 2 Then
            LoadGrid()
        Else
            MsgBox("Calen dues llistes de preus per comparar", MsgBoxStyle.Exclamation, "MAT.NET")
            Me.Close()
        End If
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = ""
        Dim oGuidX As Guid
        Dim oGuidY As Guid
        Dim PXCaption As String = ""
        Dim PYCaption As String = ""

        Select Case _Mode
            Case Modes.Supplier
                If _PriceLists_Supplier.Count < 2 Then

                End If
                oGuidX = _PriceLists_Supplier(0).Guid
                oGuidY = _PriceLists_Supplier(1).Guid
                PXCaption = _PriceLists_Supplier(0).Fch.ToShortDateString
                PYCaption = _PriceLists_Supplier(1).Fch.ToShortDateString

                SQL = "SELECT PX.Ref, PX.Description, PX.Price, PY.Price AS Expr1,  PY.Price / PX.Price - 1 AS DIFF " _
                    & "FROM            PriceListItem_Supplier AS PX INNER JOIN " _
                    & "PriceListItem_Supplier AS PY ON PX.Art = PY.Art " _
                    & "WHERE PX.PriceList = @PX AND PY.PriceList = @PY " _
                    & "ORDER BY PX.Ref"
            Case Modes.Customer
                oGuidX = _PriceLists_Customer(0).Guid
                oGuidY = _PriceLists_Customer(1).Guid
                PXCaption = _PriceLists_Customer(0).Fch.ToShortDateString
                PYCaption = _PriceLists_Customer(1).Fch.ToShortDateString
                SQL = "SELECT Product.Guid, Product.Nom, PX.TarifaA, PY.TarifaA AS Expr1,  PY.TarifaA / PX.TarifaA - 1 AS DIFF " _
                    & "FROM            DTOPricelistItemCustomer AS PX INNER JOIN " _
                    & "DTOPricelistItemCustomer AS PY ON PX.Art = PY.Art INNER JOIN " _
                    & "PRODUCT ON PX.Art = PRODUCT.GUID " _
                    & "WHERE PX.PriceList = @PX AND PY.PriceList = @PY " _
                    & "ORDER BY PRODUCT.NOM"
        End Select

        Dim oDs As DataSet = GetDataset(SQL, Databases.Maxi, "@PX", oGuidX.ToString, "@PY", oGuidY.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.ArtId)
                Select Case _Mode
                    Case Modes.Supplier
                        .HeaderText = "ref.proveidor"
                        .Width = 70
                    Case Modes.Customer
                        .Visible = False
                End Select
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.TarifaX)
                .HeaderText = PXCaption
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.TarifaY)
                .HeaderText = PYCaption
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Diff)
                .HeaderText = "increment"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "0.0 %;-0.0 %;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        'SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Diff
                Select Case e.Value
                    Case 0
                        e.CellStyle.BackColor = Color.White
                    Case Is < 0
                        e.CellStyle.BackColor = Color.LightSalmon
                    Case Is < -0.05
                        e.CellStyle.BackColor = Color.Salmon
                    Case Is > 0.05
                        e.CellStyle.BackColor = Color.Aqua
                    Case Is > 0
                        e.CellStyle.BackColor = Color.LightBlue
                End Select
        End Select
    End Sub
End Class
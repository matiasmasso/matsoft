Public Class Xl_EShopLandingpages

    Private _ControlItems As ControlItems
    Private _mode As EShopLandingpage.Modes
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        nom
        Fch
        Usr
    End Enum

    Public Shadows Sub Load(values As List(Of EShopLandingpage), oMode As EShopLandingpage.Modes)
        _mode = oMode
        _ControlItems = New ControlItems
        For Each oItem As EShopLandingpage In values
            Dim oControlItem As New ControlItem(oItem, _mode)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As EShopLandingpage
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As EShopLandingpage = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Usr)
                .HeaderText = "Aprovat per:"
                .DataPropertyName = "Usr"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function


    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_EShopLandingpage As New Menu_EShopLandingpage(SelectedItems.First)
            'AddHandler oMenu_EShopLandingpage.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_EShopLandingpage.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        'Dim oSelectedValue As EShopLandingpage = CurrentControlItem.Source
        'Dim oFrm As New Frm_EShopLandingpage(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Property Source As EShopLandingpage

        Property Nom As String
        Property Fch As Date
        Property Usr As String

        Public Sub New(oEShopLandingpage As EShopLandingpage, oMode As EShopLandingpage.Modes)
            MyBase.New()
            _Source = oEShopLandingpage
            With oEShopLandingpage
                Select Case oMode
                    Case EShopLandingpage.Modes.ProductShops
                        _Nom = .Contact.Clx
                    Case EShopLandingpage.Modes.ShopProducts
                        _Nom = BLL_Product.Nom(.Product)
                End Select
                _Fch = .FchCreated

                If .UsrApproved IsNot Nothing Then
                    If .UsrApproved.NickName > "" Then
                        _Usr = .UsrApproved.NickName
                    Else
                        _Usr = .UsrApproved.EmailAddress
                    End If
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class


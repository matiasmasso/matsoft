Public Class Xl_Tels_Old
    Private _Contact As Contact
    Private _Items As Items
    Private _AllowEvents As Boolean

    Private Enum Cols
        Icon
        Value
        Obs
    End Enum

    Public WriteOnly Property Contact As Contact
        Set(value As Contact)
            _Contact = value
            LoadGrid()
        End Set
    End Property

    Public ReadOnly Property Tels As tels
        Get
            Return New tels
        End Get
    End Property

    Public ReadOnly Property emails As Emails
        Get
            Return New Emails
        End Get
    End Property

    Private Sub LoadGrid()
        _Items = New Items
        For Each oTel As Tel In Tels.FromContact(_Contact)
            _Items.Add(New Item(oTel))
        Next
        For Each oEmail As Email In emails.FromContact(_Contact)
            _Items.Add(New Item(oEmail))
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewImageColumn())
            .Columns.Add("Value", "Value")
            .Columns.Add("Obs", "Obs")
            .DataSource = _Items
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Icon)
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Value)
                .DataPropertyName = "Value"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Obs)
                .DataPropertyName = "Obs"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentItem() As Item
        Dim retval As Item = Nothing

        Dim oRows As DataGridViewSelectedRowCollection = DataGridView1.SelectedRows
        If oRows.Count > 0 Then
            Dim oRow As DataGridViewRow = oRows(0)
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oItem As Item = CurrentItem()

        If oItem IsNot Nothing Then
            Select Case oItem.Cod
                Case Item.Cods.Tel
                    Dim oMenu_Tel As New Menu_Tel(CType(oItem.Source, Tel))
                    AddHandler oMenu_Tel.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Tel.Range)
                Case Item.Cods.Email
                    Dim oMenu_Email As New Menu_Email(CType(oItem.Source, Email))
                    AddHandler oMenu_Email.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Email.Range)
            End Select
            oContextMenu.Items.Add("-")
        End If

        Dim oMenuItem As ToolStripMenuItem = oContextMenu.Items.Add("afegir")
        With oMenuItem.DropDownItems
            .Add("telefon", Nothing, AddressOf Do_AddNewTelefon)
            .Add("mobil", Nothing, AddressOf Do_AddNewMobil)
            .Add("fax", Nothing, AddressOf Do_AddNewFax)
            .Add("email", Nothing, AddressOf Do_AddNewEmail)
        End With

        If oItem IsNot Nothing Then
            Select Case oItem.Cod
                Case Item.Cods.Tel
                Case Item.Cods.Email
                    oContextMenu.Items.Add("-")
                    oContextMenu.Items.Add("desvincular d'aquest contacte", Nothing, AddressOf Do_Desvincular)
            End Select
        End If


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNewTelefon()
        Dim oTel As New Tel(_Contact, MaxiSrvr.Tel.Cods.tel)
        Dim oFrm As New Frm_Tel_Old(oTel)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNewMobil()
        Dim oTel As New Tel(_Contact, MaxiSrvr.Tel.Cods.movil)
        Dim oFrm As New Frm_Tel_Old(oTel)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNewFax()
        Dim oTel As New Tel(_Contact, MaxiSrvr.Tel.Cods.fax)
        Dim oFrm As New Frm_Tel_Old(oTel)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNewEmail()
        Dim BlAllowToAdd As Boolean = False
        If _Contact.Exists Then
            BlAllowToAdd = True
        Else
            Dim exs As New List(Of Exception)
            If _Contact.UpdateGral(exs) Then
                BlAllowToAdd = True
            Else
                MsgBox("fallo al grabar el contacte per donar d'alta un correu" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End If

        If BlAllowToAdd Then
            Dim oEmail As New Email(_Contact.ToDTO)
            Dim oFrm As New Frm_Contact_Email(oEmail)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_Desvincular()
        Dim oItem As Item = CurrentItem()
        Dim oEmail As Email = oItem.Source
        Dim exs As New List(Of Exception)
        If oEmail.RemoveContact(_Contact, exs) Then
            RefreshRequest()
        Else
            MsgBox("No s'ha pogut retirar el contacte", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub RefreshRequest()
        LoadGrid()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Icon
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oItem As Item = oRow.DataBoundItem
                Select Case oItem.Icon
                    Case Item.Icons.tel
                        e.Value = My.Resources.tel
                    Case Item.Icons.fax
                        e.Value = My.Resources.fax
                    Case Item.Icons.movil
                        e.Value = My.Resources.movil
                    Case Item.Icons.email
                        e.Value = My.Resources.MailSobreGroc
                    Case Item.Icons.PrivateTel
                        e.Value = My.Resources.tel_red
                    Case Item.Icons.PrivateMovil
                        e.Value = My.Resources.movil_red
                    Case Item.Icons.emailBadMail
                        e.Value = My.Resources.wrong
                    Case Item.Icons.emailNoMailings
                        e.Value = My.Resources.NoPark
                    Case Item.Icons.emailEfras
                        e.Value = My.Resources.MailSobreGrocBlau
                    Case Item.Icons.Obsolet
                        e.Value = My.Resources.del
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oItem As Item = CurrentItem()
        Select Case oItem.Cod
            Case Item.Cods.Tel
                Dim oTel As Tel = oItem.Source
                Dim oFrm As New Frm_Tel_Old(oTel)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Item.Cods.Email
                Dim oEmail As Email = oItem.Source
                Dim oFrm As New Frm_Contact_Email(oEmail)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Protected Class Item
        Public Property Source As Object
        Public Property Cod As Cods
        Public Property Icon As Icons
        Public Property Value As String
        Public Property Obs As String
        Public Property IsNew As Boolean

        Public Enum Cods
            NotSet
            Tel
            Email
        End Enum

        Public Enum Icons
            NotSet
            tel
            fax
            movil
            email
            PrivateTel
            PrivateMovil
            emailBadMail
            emailNoMailings
            emailEfras
            Obsolet
        End Enum

        Public Sub New(oTel As Tel)
            MyBase.New()
            _Source = oTel
            _Cod = Cods.Tel
            _Icon = oTel.Cod

            If oTel.Privat = DTOEnums.TriState.Verdadero Then
                If oTel.Cod = Tel.Cods.tel Then _Icon = Icons.PrivateTel
                If oTel.Cod = Tel.Cods.movil Then _Icon = Icons.PrivateMovil
            End If

            _Value = oTel.formatted
            _Obs = oTel.Obs
        End Sub

        Public Sub New(oEmail As Email)
            MyBase.New()
            _Source = oEmail
            _Cod = Cods.Email
            _Icon = Icons.email
            _Value = oEmail.Adr
            _Obs = oEmail.Nom

            If oEmail.xObsoleto = DTOEnums.TriState.Verdadero Then
                _Icon = Icons.Obsolet
            ElseIf oEmail.BadMail <> Email.BadMailErrs.None Then
                _Icon = Icons.emailBadMail
            ElseIf oEmail.Efras Then
                _Icon = Icons.emailEfras
            ElseIf oEmail.NoNews Then
                _Icon = Icons.emailNoMailings
            End If
        End Sub

    End Class

    Protected Class Items
        Inherits System.Collections.CollectionBase

        Public Sub Add(ByVal NewObjMember As Item)
            List.Add(NewObjMember)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Item
            Get
                Item = List.Item(vntIndexKey)
            End Get
        End Property

        Public Sub InsertAt(index As Integer, ByVal NewObjMember As Item)
            List.Insert(index, NewObjMember)
        End Sub

        Public Sub Remove(oObjectToRemove As Item)
            List.Remove(oObjectToRemove)
        End Sub
    End Class


End Class





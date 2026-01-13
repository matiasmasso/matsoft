Public Class Xl_LookupIncoterm

    Inherits ComboBox

    Private _Incoterms As List(Of DTOIncoterm)
    Private _NullValue As DTOIncoterm
    Private _Allowevents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oIncoterms As List(Of DTOIncoterm), Optional DefaultValue As DTOIncoterm = Nothing)
        _NullValue = DTOIncoterm.Factory("   ")
        _Incoterms = oIncoterms
        _Incoterms.Insert(0, _NullValue)
        MyBase.DataSource = _Incoterms
        MyBase.DisplayMember = "Id"
        MyBase.ValueMember = "Id"
        Me.Value = DefaultValue
        SetContextMenu()
        _Allowevents = True
    End Sub

    Public Property Value As DTOIncoterm
        Get
            Return MyBase.SelectedItem
        End Get
        Set(value As DTOIncoterm)
            refresca(value)
        End Set
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If Me.Value IsNot Nothing AndAlso Not Me.Value.Equals(_NullValue) Then
            Dim oMenu_Incoterm As New Menu_Incoterm(Me.Value)
            AddHandler oMenu_Incoterm.AfterUpdate, AddressOf refresca
            oContextMenu.Items.AddRange(oMenu_Incoterm.Range)

            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        MyBase.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub Do_AddNew()
        Dim oValue As New DTOIncoterm
        Dim oFrm As New Frm_Incoterm(oValue)
        AddHandler oFrm.AfterUpdate, AddressOf onNewItemAdded
        oFrm.Show()
    End Sub

    Private Sub onNewItemAdded(sender As Object, e As MatEventArgs)
        Dim oNewIncoterm As DTOIncoterm = e.Argument
        _Incoterms.Add(oNewIncoterm)
        _Incoterms = _Incoterms.OrderBy(Function(x) x.Id).ToList()
        MyBase.DataSource = _Incoterms
        refresca(oNewIncoterm)
    End Sub

    Private Sub refresca(sender As Object, e As MatEventArgs)
        refresca(e.Argument)
    End Sub

    Private Sub refresca(value As DTOIncoterm)
        If value Is Nothing Then
            If MyBase.Items.Count > 0 Then
                MyBase.SelectedIndex = 0
            End If
        Else
            MyBase.SelectedValue = value.Id
        End If
    End Sub


    Private Sub Xl_LookupIncoterm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        If _Allowevents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
        End If
    End Sub

    Private Sub Xl_LookupIncoterm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Right Then
            SetContextMenu()
        End If
    End Sub
End Class
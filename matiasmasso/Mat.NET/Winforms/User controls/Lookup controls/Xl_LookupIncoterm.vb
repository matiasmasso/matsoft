Public Class Xl_LookupIncoterm

    Inherits ComboBox

    Private _NullValue As DTOIncoterm
    Private _Allowevents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oIncoterms As List(Of DTOIncoterm), Optional DefaultValue As DTOIncoterm = Nothing)
        _NullValue = DTOIncoterm.Factory("   ")
        oIncoterms.Insert(0, _NullValue)
        MyBase.DataSource = oIncoterms
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
            If value Is Nothing Then
                If MyBase.Items.Count > 0 Then
                    MyBase.SelectedIndex = 0
                End If
            Else
                MyBase.SelectedValue = value.Id
            End If
        End Set
    End Property

    Private Sub SetContextMenu()
        If Me.Value IsNot Nothing AndAlso Not Me.Value.Equals(_NullValue) Then
            'menu_Incoterm
        End If
        'addnew
    End Sub

    Private Sub Xl_LookupIncoterm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        If _Allowevents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
        End If
    End Sub
End Class


Public Class Xl_Art_Group
    Private _Art As Art
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Art() As Art
        Set(ByVal value As Art)
            _Art = value
            Dim BlGroupParentOnly As Boolean = _Art.GroupParentOnly
            CheckBoxGroupParentOnly.Checked = BlGroupParentOnly
            Xl_ArtWiths1.Load(_Art.ArtWiths)
            _AllowEvents = True
        End Set
    End Property

    Public ReadOnly Property Items As ArtWiths
        Get
            Dim retval As ArtWiths = Xl_ArtWiths1.Items
            Return retval
        End Get
    End Property

    Public ReadOnly Property GroupParentOnly As Boolean
        Get
            Dim retval As Boolean
            If CheckBoxGroupParentOnly.Checked Then
                If Xl_ArtWiths1.Items.Count > 0 Then
                    retval = True
                End If
            End If
            Return retval
        End Get
    End Property

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        Xl_ArtWiths1.AfterUpdate, _
         CheckBoxGroupParentOnly.CheckedChanged

        If _AllowEvents Then SetDirty()
    End Sub


    Private Sub SetDirty()
        RaiseEvent AfterUpdate(Nothing, New System.EventArgs)
    End Sub
End Class

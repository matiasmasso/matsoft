Public Class Xl_CustomerCluster
    Inherits ComboBox

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    ReadOnly Property Value() As DTOCustomer.Clusters
        Get
            Dim retval As DTOCustomer.Clusters = MyBase.SelectedIndex
            Return retval
        End Get
    End Property

    Public Shadows Sub Load(oCluster As DTOCustomer.Clusters)
        MyBase.DataSource = ({"-", "A", "B", "C", "D", "E", "F"})
        MyBase.SelectedIndex = oCluster
        _AllowEvents = True
    End Sub

    Private Sub onItemSelected(sender As Object, e As EventArgs) Handles MyBase.SelectedIndexChanged
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(MyBase.SelectedIndex))
        End If
    End Sub
End Class

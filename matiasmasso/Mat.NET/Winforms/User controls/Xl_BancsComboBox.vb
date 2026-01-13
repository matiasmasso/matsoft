Public Class Xl_BancsComboBox
    Inherits ComboBox

    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Public Sub Load(oBancs As List(Of DTOBanc), oDefaultBanc As DTOBanc)
        MyBase.DataSource = oBancs
        MyBase.DisplayMember = "Abr"
        If oDefaultBanc IsNot Nothing Then
            Dim oSelectedItem As DTOBanc = oBancs.Find(Function(x) x.Equals(oDefaultBanc))
            If oSelectedItem Is Nothing Then
                oSelectedItem = oDefaultBanc
                oBancs.Add(oSelectedItem)
            End If
            MyBase.SelectedItem = oSelectedItem
        End If
        _AllowEvents = True
    End Sub

    Public Async Function LoadDefaultsFor(oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oBancs As List(Of DTOBanc) = Await FEB2.Bancs.AllActive(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Dim oGuid As Guid = Await FEB2.Default.EmpGuid(Current.Session.Emp, oCod, exs)
            If exs.Count = 0 Then
                Dim oDefaultBanc = Nothing
                If oGuid <> Guid.Empty Then
                    oDefaultBanc = New DTOBanc(oGuid)
                End If
                Load(oBancs, oDefaultBanc)
            End If
        End If
        Return exs.Count = 0
    End Function


    Private Sub Xl_BancsComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Me.SelectedIndexChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(MyBase.SelectedItem))
        End If
    End Sub
End Class

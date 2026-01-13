Public Class Xl_ProductChannelsTree
    Inherits TreeView

    Private _Values As List(Of DTOProductChannel)
    Private _AllowEvents As Boolean

    Public Shadows Sub Load(values As List(Of DTOProductChannel))
        _Values = values
        MyBase.Nodes.Clear()
        refresca
    End Sub

    Private Sub refresca()

    End Sub

    Private Function ChannelQuery() As List(Of DTODistributionChannel)
        Dim retval As List(Of DTODistributionChannel) = _Values.
            GroupBy(Function(g) New With {Key g.DistributionChannel.Guid,
                                          Key g.DistributionChannel.LangText.Esp,
                                          Key g.DistributionChannel.LangText.Cat,
                                          Key g.DistributionChannel.LangText.Eng,
                                          Key g.DistributionChannel.LangText.Por
                                            }).
                Select(Function(group) New DTODistributionChannel With {
                    .Guid = group.Key.Guid,
                    .LangText = New DTOLangText(group.Key.Esp, group.Key.Cat, group.Key.Eng, group.Key.Por)
                           }).ToList
        Return retval
    End Function
End Class

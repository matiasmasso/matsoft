Public Class ExpandableItem
    Public Property Guid As Guid
    Public Property CellSpan As CellSpanCods = CellSpanCods.None
    Public Property BackgroundColor As Color
    Public Property Caption As String
    Public Property Status As Statuses
    Public Property Level As Integer
    Public Property LinCod As Integer
    Public Property Tag As Object

    Public Enum Statuses
        None
        Collapsed
        Expanded
    End Enum

    Public Enum CellSpanCods
        None
        SpanAll
        SpanIfCollapsed
    End Enum

    Public Sub New(oGuid As Guid, iLincod As Integer, sCaption As String, iLevel As Integer, Optional oStatus As Statuses = Statuses.None)
        MyBase.New
        Guid = oGuid
        LinCod = iLincod
        Caption = sCaption
        Level = iLevel
        Status = oStatus
    End Sub

    Public Function IsExpanded() As Boolean
        Return Status = Statuses.Expanded
    End Function

    Public Function IsCollapsed() As Boolean
        Return Status = Statuses.Collapsed
    End Function

    Public Function HasIcon() As Boolean
        Return Status <> Statuses.None
    End Function

    Public Sub Toggle()
        Select Case Status
            Case Statuses.Expanded
                Status = Statuses.Collapsed
            Case Statuses.Collapsed
                Status = Statuses.Expanded
        End Select
    End Sub

    Public Sub CollapseNestedLevels(oControlItems As SortableBindingList(Of ExpandableItem))
        Dim idx = oControlItems.IndexOf(Me) + 1
        Do While oControlItems.Count > idx
            If oControlItems(idx).Level <= Level Then Exit Do
            oControlItems.RemoveAt(idx)
        Loop
    End Sub

    Public Shadows Function ToString() As String
        Return Caption
    End Function


End Class

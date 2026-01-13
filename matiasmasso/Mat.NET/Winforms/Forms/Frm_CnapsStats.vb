Public Class Frm_CnapsStats
    Private _Stat As DTOStat
    Private _Lang As DTOLang
    Private _Cnaps As List(Of DTOCnap)

    Private Sub Frm_CnapsStats_Load(sender As Object, e As EventArgs) Handles Me.Load
        _Lang = BLLSession.Current.Lang
        LoadYears()
        Dim oCnaps As List(Of DTOCnap) = BLLCnaps.All()
        Xl_CnapsCheckTree1.Load(oCnaps, _Lang)
        _Stat = New DTOStat(DTOStat.ConceptTypes.Product, _Lang)
        With _Stat
            .ExpandToLevel = 2
        End With
        BLLStat.Load2(_Stat)
        refresca()
    End Sub

    Private Sub LoadYears()
        Dim iYears As New List(Of Integer)
        Dim iYear As Integer = BLLExercici.Current.Year
        For i As Integer = iYear To 1985 Step -1
            iYears.Add(i)
        Next
        Xl_Years1.Load(iYears, iYear)
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        With _Stat
            .Year = Xl_Years1.Value
        End With
        BLLStat.Load2(_Stat)
        refresca()
    End Sub

    Private Sub refresca()
        _Cnaps = Xl_CnapsCheckTree1.CheckedValues
        Dim oStat As New DTOStat(DTOStat.ConceptTypes.Product, _Lang)
        With oStat
            .ExpandToLevel = 2
            .Items = FilteredItems()
        End With
        Xl_Stats1.Load(oStat)
    End Sub

    Private Sub Xl_CnapsCheckTree1_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles Xl_CnapsCheckTree1.AfterCheck
        If Xl_CnapsCheckTree1.IgnoreClickAction = 0 Then
            refresca()
        End If
    End Sub

    Private Function FilteredItems() As List(Of DTOStatItem)

        Dim retval As List(Of DTOStatItem)
        If _Cnaps Is Nothing Then
            retval = New List(Of DTOStatItem)
        Else
            retval = _Stat.Items.Where(Function(x) Matches(x)).ToList
        End If
        Return retval
    End Function

    Private Function Matches(item As DTOStatItem) As Boolean
        Dim retval As Boolean
        If TypeOf item.Tag Is DTOProductSku Then
            Dim oSku As DTOProductSku = item.Tag
            If oSku.Cnap IsNot Nothing Then
                retval = _Cnaps.Any(Function(x) x.Id.StartsWith(oSku.Cnap.Id))
            End If
        End If
        Return retval
    End Function
End Class
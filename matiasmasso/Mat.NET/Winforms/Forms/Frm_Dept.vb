Public Class Frm_Dept
    Private _Dept As DTODept
    Private _AllFilters As DTOFilter.Collection = Nothing
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Gral
        Cnaps
        Categories
        Banners
        FilterUrls
    End Enum

    Public Sub New(value As DTODept)
        MyBase.New()
        Me.InitializeComponent()
        _Dept = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As EventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _Dept.IsLoaded = False
        If FEB2.Dept.Load(_Dept, True, exs) Then
            Dim oCnaps = Await FEB2.Cnaps.Tree(exs)
            If exs.Count = 0 Then
                With _Dept
                    TextBoxBrandNom.Text = .brand.Nom.Tradueix(Current.Session.Lang)
                    TextBoxEsp.Text = .Nom.Tradueix(Current.Session.Lang)
                    NumericUpDownOrd.Value = .Ord
                    Xl_CnapsCheckTree1.Load(oCnaps, .cnaps, Current.Session.Lang)
                    Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(.banner)
                    ButtonOk.Enabled = .IsNew
                    ButtonDel.Enabled = Not .IsNew
                End With
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
                Me.Close()
            End If
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
            NumericUpDownOrd.ValueChanged,
            Xl_Image1.AfterUpdate,
             Xl_CnapsCheckTree1.AfterCheck

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Dept
            .Ord = NumericUpDownOrd.Value
            .banner = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            .cnaps = Xl_CnapsCheckTree1.CheckedValues
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Dept.Upload(_Dept, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Dept))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Dept.Delete(_Dept, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Dept))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Categories
                Xl_ProductCategories2.load(_Dept.Categories)
            Case Tabs.FilterUrls
                If _AllFilters Is Nothing Then _AllFilters = Await FEB2.Filters.All(exs)
                Dim oFilterItems As New DTOFilter.Item.Collection
                oFilterItems.AddRange(_Dept.Categories.SelectMany(Function(x) x.FilterItems))
                Dim oFilterTree As DTOFilter.Collection = DTOFilter.Collection.Tree(_AllFilters, oFilterItems)
                Xl_ProductFilteredUrls1.Load(_Dept, oFilterTree)
        End Select
    End Sub

    Private Sub ButtonShowLangTexts_Click(sender As Object, e As EventArgs) Handles ButtonShowLangTexts.Click
        Dim oFrm As New Frm_ProductDescription(_Dept)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_CnapsCheckTree1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_CnapsCheckTree1.ValueChanged
        Dim oCnap As DTOCnap = Xl_CnapsCheckTree1.SelectedClass()
        Dim oCategories = oCnap.MatchingCategories(_Dept.Categories)
        Xl_ProductCategories1.load(oCategories)
    End Sub
End Class

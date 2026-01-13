Public Class Frm_Cnaps
    Private _SelectionMode As BLL.Defaults.SelectionModes
    Private _DefaultCnap As DTOCnap = Nothing
    Private _AllowEvents As Boolean

    Private Enum Tabs
        Productes
        Keywords
    End Enum

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(Optional ByVal oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse, Optional ByVal oDefaultCnap As DTOCnap = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _DefaultCnap = oDefaultCnap
    End Sub

    Private Sub Frm_Cnaps_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oCnaps As List(Of DTOCnap) = CnapsLoader.FromRoot()
        Xl_CnapTree1.Load(oCnaps, _DefaultCnap, _SelectionMode)
        _AllowEvents = True
    End Sub

    Public Function CurrentCnap() As DTOCnap
        Dim retval As DTOCnap = Xl_CnapTree1.Cnap
        Return retval
    End Function


    Private Sub Xl_CnapTree1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CnapTree1.onItemSelected
        If _AllowEvents Then
            If _SelectionMode = BLL.Defaults.SelectionModes.Selection Then
                RaiseEvent AfterSelect(sender, e)
                Me.Close()
            End If
        End If

    End Sub

End Class
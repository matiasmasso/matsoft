Public Class Frm_Cnaps
    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _DefaultCnap As DTOCnap = Nothing
    Private _AllowEvents As Boolean

    Private Enum Tabs
        Productes
        Keywords
    End Enum

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(Optional ByVal oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse, Optional ByVal oDefaultCnap As DTOCnap = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _DefaultCnap = oDefaultCnap
    End Sub

    Private Async Sub Frm_Cnaps_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oCnaps = Await FEB.Cnaps.Tree(exs)
        If exs.Count = 0 Then
            Xl_CnapTree1.Load(oCnaps, _DefaultCnap, _SelectionMode)
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Public Function CurrentCnap() As DTOCnap
        Dim retval As DTOCnap = Xl_CnapTree1.Cnap
        Return retval
    End Function


    Private Sub Xl_CnapTree1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CnapTree1.onItemSelected
        If _AllowEvents Then
            If _SelectionMode = DTO.Defaults.SelectionModes.Selection Then
                RaiseEvent AfterSelect(sender, e)
                Me.Close()
            End If
        End If

    End Sub

End Class
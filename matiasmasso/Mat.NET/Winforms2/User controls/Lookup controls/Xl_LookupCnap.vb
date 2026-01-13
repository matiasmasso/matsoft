Public Class Xl_LookupCnap
    Inherits Xl_LookupTextboxButton

    Private _Cnap As DTOCnap
    Private _inheritedCnap As DTOCnap

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows ReadOnly Property Cnap() As DTOCnap
        Get
            Return _Cnap
        End Get
    End Property

    Public Shadows Sub Load(oCnap As DTOCnap, Optional inheritedCnap As DTOCnap = Nothing)
        _Cnap = oCnap
        _inheritedCnap = inheritedCnap
        refresca()
    End Sub

    Private Sub SetColor()
        Dim inherited As Boolean = _Cnap Is Nothing And _inheritedCnap IsNot Nothing
        Dim matchesInherited As Boolean
        If _Cnap IsNot Nothing Then
            matchesInherited = _Cnap.Equals(_inheritedCnap)
        End If
        If (inherited Or matchesInherited) Then
            MyBase.TextBox1.BackColor = Color.LightGray
        Else
            MyBase.TextBox1.BackColor = Color.White
        End If
    End Sub
    Public Sub Clear()
        _Cnap = Nothing
        refresca()
    End Sub

    Private Sub Xl_LookupCnap_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _Cnap IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If Not FEB.Cnap.Load(_Cnap, exs) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If

        Dim oFrm As New Frm_Cnaps(DTO.Defaults.SelectionModes.Selection, _Cnap)
        AddHandler oFrm.AfterSelect, AddressOf onCnapSelected
        oFrm.Show()
    End Sub

    Private Sub onCnapSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Cnap = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Cnap Is Nothing Then
            If _inheritedCnap Is Nothing Then
                MyBase.Text = ""
                MyBase.ClearContextMenu()
            Else
                MyBase.Text = _inheritedCnap.FullNom(Current.Session.Lang)
                Dim oMenu_Cnap As New Menu_Cnap(_inheritedCnap)
                AddHandler oMenu_Cnap.AfterUpdate, AddressOf refresca
                MyBase.SetContextMenuRange(oMenu_Cnap.Range)

            End If
        Else
            MyBase.Text = _Cnap.FullNom(Current.Session.Lang)
            Dim oMenu_Cnap As New Menu_Cnap(_Cnap)
            AddHandler oMenu_Cnap.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Cnap.Range)
        End If
        SetColor()
    End Sub

    Private Sub Xl_LookupCnap_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_Cnap(_Cnap)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class

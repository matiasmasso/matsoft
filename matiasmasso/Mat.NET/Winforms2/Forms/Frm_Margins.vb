Public Class Frm_Margins

    Private _Mode As Models.MarginsModel.Modes
    Private _Target As Object
    Private _Value As Models.MarginsModel
    Private _AllowEvents As Boolean


    Public Sub New(Optional oMode As Models.MarginsModel.Modes = Models.MarginsModel.Modes.Full, Optional oTarget As Object = Nothing)
        MyBase.New
        InitializeComponent()
        _Mode = oMode
        _Target = oTarget
        Xl_Years1.LoadFrom(1985)
    End Sub


    Private Async Sub Frm_CustomerPmcs_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _Target IsNot Nothing Then
            If TypeOf _Target Is DTOContact Then
                Me.Text = String.Format("{0} {1}", Me.Text, CType(_Target, DTOContact).FullNom)
            End If
        End If
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Value = Await FEB.Margins.Fetch(exs, GlobalVariables.Emp, Xl_Years1.Value, _Mode, _Target)
        If exs.Count = 0 Then
            Xl_Marges1.Load(_Value, Xl_Marges.Modes.Brands)
            Xl_Marges2.Load(_Value, Xl_Marges.Modes.Categories, Xl_Marges1.SelectedItem)
            Xl_Marges3.Load(_Value, Xl_Marges.Modes.Skus, Xl_Marges2.SelectedItem)
            Xl_Marges4.Load(_Value, Xl_Marges.Modes.Items, Xl_Marges3.SelectedItem)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
        _AllowEvents = True

    End Function


    Private Sub Xl_Marges_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Marges1.ValueChanged, Xl_Marges2.ValueChanged, Xl_Marges3.ValueChanged
        If _AllowEvents Then
            Dim oControl As Xl_Marges = sender
            Dim oTarget = e.Argument
            Select Case oControl.Mode
                Case Xl_Marges.Modes.Brands
                    Xl_Marges2.Load(_Value, Xl_Marges.Modes.Categories, oTarget)
                    Xl_Marges3.Load(_Value, Xl_Marges.Modes.Skus, Xl_Marges2.SelectedItem)
                    Xl_Marges4.Load(_Value, Xl_Marges.Modes.Items, Xl_Marges3.SelectedItem)
                Case Xl_Marges.Modes.Categories
                    Xl_Marges3.Load(_Value, Xl_Marges.Modes.Skus, oTarget)
                    Xl_Marges4.Load(_Value, Xl_Marges.Modes.Items, Xl_Marges3.SelectedItem)
                Case Xl_Marges.Modes.Skus
                    Xl_Marges4.Load(_Value, Xl_Marges.Modes.Items, oTarget)
            End Select
        End If
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub
End Class
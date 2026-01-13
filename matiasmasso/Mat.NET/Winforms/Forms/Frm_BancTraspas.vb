Public Class Frm_BancTraspas
    Private _Banc As DTOBanc
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oBanc As DTOBanc = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Banc = oBanc
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oBancs = Await FEB2.Bancs.AllActive(Current.Session.Emp, exs)

        Dim oBancsEmissors As New List(Of DTOBanc)
        oBancsEmissors.AddRange(oBancs)
        If _Banc IsNot Nothing Then
            If Not oBancsEmissors.Any(Function(x) x.Equals(_Banc)) Then
                oBancsEmissors.Add(_Banc)
                oBancsEmissors = oBancsEmissors.OrderBy(Function(x) x.AbrOrNom).ToList
            End If
        End If

        If exs.Count = 0 Then
            ComboBoxBancEmissor.DataSource = oBancsEmissors
            ComboBoxBancEmissor.DisplayMember = "Abr"
            If _Banc IsNot Nothing Then
                ComboBoxBancEmissor.SelectedItem = oBancsEmissors.Find(Function(x) x.Equals(_Banc))
            End If

            Dim oBancsReceptors As New List(Of DTOBanc)
            oBancsReceptors.AddRange(oBancs)
            ComboBoxBancReceptor.DataSource = oBancsReceptors
            ComboBoxBancReceptor.DisplayMember = "Abr"
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Validate(exs) Then

            Dim oTraspas As DTOBancTransferPool = Await FEB2.BancTransferPool.Traspas(
            Current.Session.User,
            ComboBoxBancEmissor.SelectedItem,
            ComboBoxBancReceptor.SelectedItem,
            Xl_EurImport.Amt,
            Xl_EurExpenses.Amt,
            DateTimePicker1.Value,
            exs)

            If exs.Count = 0 Then
                Dim CcaId = Await FEB2.BancTransferPool.Save(exs, oTraspas)
                If exs.Count = 0 Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(oTraspas))
                    Dim XMLSource As String = Await FEB2.SepaCreditTransfer.XML(Current.Session.Emp, oTraspas, exs)
                    If exs.Count = 0 Then
                        Dim sFilename As String = oTraspas.DefaultFilename()
                        UIHelper.SaveXmlFileDialog(XMLSource, sFilename)
                        Me.Close()
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs, "error al desar el traspas")
                End If
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Shadows Function Validate(exs As List(Of Exception)) As Boolean
        If BancReceptor.Equals(BancEmissor) Then
            exs.Add(New Exception("els bancs emissor i receptor han de ser diferents"))
        End If
        If Not Xl_EurImport.Amt.IsPositive Then
            exs.Add(New Exception("l'import ha de ser positiu"))
        End If
        If DateTimePicker1.Value < Today Then
            exs.Add(New Exception("la data ha de ser a partir d'avui"))
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Private Function BancEmissor() As DTOBanc
        Dim retval As DTOBanc = ComboBoxBancEmissor.SelectedItem
        Return retval
    End Function
    Private Function BancReceptor() As DTOBanc
        Dim retval As DTOBanc = ComboBoxBancReceptor.SelectedItem
        Return retval
    End Function

End Class



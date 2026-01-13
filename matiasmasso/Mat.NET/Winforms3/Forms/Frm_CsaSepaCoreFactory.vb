Public Class Frm_CsaSepaCoreFactory
    Private _Csbs As List(Of DTOCsb)
    Private _Format As DTOCsa.FileFormats
    Private _AllowEvents As Boolean

    Private _Banc As DTOBanc

    Public Sub New(oBanc As DTOBanc, oFormat As DTOCsa.FileFormats)
        MyBase.New
        InitializeComponent()

        _Banc = oBanc
        _Format = oFormat
    End Sub

    Private Sub Frm_SepaCoreCsaFactory_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Banc.Load(_Banc, exs) Then
            Select Case _Format
                Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                    Me.Text = "Remesa de exportació La Caixa"
                Case Else
                    Me.Text = Me.Text & " " & _Banc.Abr
            End Select
        Else
            UIHelper.WarnError(exs)
        End If
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Select Case _Format
            Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                UIHelper.ToggleProggressBar(Panel1, True)
                Dim oAllCsbs = Await FEB.Csbs.PendentsDeGirar(exs, GlobalVariables.Emp, oCountry:=Nothing, sepa:=False)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count = 0 Then
                    Dim oNone As List(Of DTOCsb) = oAllCsbs.FindAll(Function(x) x.Iban Is Nothing)
                    If oNone.Count = 0 Then
                        _Csbs = oAllCsbs.FindAll(Function(x) Not x.Iban.Digits.StartsWith("ES"))
                    Else
                        WarnNoIban(exs, oNone)
                        If exs.Count = 0 Then
                            Me.Close()
                            Exit Sub
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    End If
                Else
                    UIHelper.WarnError(exs)
                    Me.Close()
                    Exit Sub
                End If
            Case Else
                UIHelper.ToggleProggressBar(Panel1, True)
                _Csbs = Await FEB.Csbs.PendentsDeGirar(exs, GlobalVariables.Emp, oCountry:=Nothing, sepa:=True)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                    Me.Close()
                    Exit Sub
                End If

        End Select
        DTOCsb.Validate(_Csbs, _Format)

        _AllowEvents = False
        Xl_CsbVtos1.Load(_Csbs)
        Dim oCsbs As List(Of DTOCsb) = Xl_CsbVtos1.SelectedVtoCsbs
        Xl_Csbs_Checklist1.Load(oCsbs)

        SetStatus()
        _AllowEvents = True
    End Sub

    Private Sub WarnNoIban(exs As List(Of Exception), oCsbs As List(Of DTOCsb))
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Les següents partides tenen problemes amb l'Iban:")
        For Each oCsb As DTOCsb In oCsbs
            If FEB.Pnd.Load(oCsb.Pnd, exs) Then
                sb.AppendLine(DTOPnd.FacturaText(oCsb.Pnd, Current.Session.Lang) & " " & oCsb.Pnd.Contact.FullNom)
            End If
        Next
        Dim s As String = sb.ToString
        MsgBox(s)
    End Sub

    Private Sub SetStatus()
        Dim oCsbs As List(Of DTOCsb) = Xl_Csbs_Checklist1.Values
        Dim oGirat = DTOCsb.TotalNominal(oCsbs)
        Dim oDisponible As DTOAmt = DTOAmt.Factory(_Csbs.Sum(Function(x) x.Amt.Eur))
        Dim src As String = String.Format("Disponible {0}. Girat {1} efectes total {2}", DTOAmt.CurFormatted(oDisponible), oCsbs.Count, DTOAmt.CurFormatted(oGirat))
        ToolStripStatusLabel1.Text = src
    End Sub

    Private Sub Xl_CsbVtos1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_CsbVtos1.ValueChanged
        If _AllowEvents Then
            Dim oCsbs As List(Of DTOCsb) = e.Argument
            Xl_Csbs_Checklist1.Load(oCsbs)
            SetStatus()
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim oCsa = DTOCsa.Factory(Current.Session.Emp, _Banc, _Format, False)
        For Each item As DTOCsb In Xl_Csbs_Checklist1.Values
            item.Csa = oCsa
            oCsa.Items.Add(item)
        Next

        Dim exs As New List(Of Exception)
        If Await FEB.Csa.SaveRemesaCobrament(oCsa, Current.Session.User, exs) Then
            UIHelper.ToggleProggressBar(Panel1, False)
            Select Case oCsa.FileFormat
                Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                    Dim textStream = Await FEB.Csa.LaCaixaRemesaExportacio(oCsa, exs)
                    If exs.Count = 0 Then
                        Dim sFilename As String = oCsa.filename()
                        UIHelper.SaveTextFileDialog(textStream, sFilename)
                        refresca()
                    Else
                        UIHelper.WarnError(exs)
                    End If


                    ' Dim src As String = BLLLaCaixaRemesaExportacio.Text(oCsa)
                    ' UIHelper.SaveTextFileDialog(src, sFilename)
                    'Dim oldCsa As New Csa(oCsa.Guid)
                    'UIHelper.SaveTextFileDialog(oldCsa.FileText, sFilename)
                Case Else
                    If FEB.Csa.Load(oCsa, exs) Then 'per recuperar Id despres de desar
                        Dim XML As String = LegacyHelper.SepaCoreHelper.SepaCoreXML(exs, Current.Session.Emp, oCsa)
                        If exs.Count = 0 Then
                            Dim sFilename As String = oCsa.filename()
                            UIHelper.SaveXmlFileDialog(XML, sFilename)
                            refresca()
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Csbs_Checklist1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Csbs_Checklist1.AfterUpdate
        If _AllowEvents Then
            SetStatus()
        End If
    End Sub
End Class
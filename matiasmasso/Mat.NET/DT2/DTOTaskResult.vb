Public Class DTOTaskResult
    Property ResultCod As DTOTask.ResultCods
    Property Msg As String
    Property Exceptions As List(Of Exception)

    Public Sub New()
        MyBase.New
        _Exceptions = New List(Of Exception)
    End Sub

    Shared Function Factory(oResultCod As DTOTask.ResultCods, Optional msg As String = "", Optional exs As List(Of Exception) = Nothing) As DTOTaskResult
        Dim retval As New DTOTaskResult
        retval.ResultCod = oResultCod
        retval.Msg = msg
        If exs IsNot Nothing Then
            retval.AddExceptions(exs)
        End If
        Return retval
    End Function

    Public Function Success() As Boolean
        Dim retval As Boolean
        Select Case _ResultCod
            Case DTOTask.ResultCods.Success, DTOTask.ResultCods.Empty
                retval = True
        End Select
        Return retval
    End Function

    Shared Function SuccessResult(Optional msg As String = "") As DTOTaskResult
        Dim retval As New DTOTaskResult
        retval.ResultCod = DTOTask.ResultCods.Success
        retval.Msg = msg
        Return retval
    End Function

    Shared Function FailResult(exs As List(Of System.Exception), Optional msg As String = "") As DTOTaskResult
        Dim retval As New DTOTaskResult
        With retval
            .ResultCod = DTOTask.ResultCods.Failed
            .AddExceptions(exs)
            .Msg = msg
        End With
        Return retval
    End Function

    Public Sub Fail()
        _ResultCod = DTOTask.ResultCods.Failed
    End Sub

    Public Sub Fail(stringFormatMsg As String, ParamArray stringFormatValues() As String)
        Fail()
        If stringFormatValues.Length > 0 Then
            _Msg = String.Format(stringFormatMsg, stringFormatValues)
        Else
            _Msg = stringFormatMsg
        End If
    End Sub

    Public Sub Fail(ex As System.Exception, stringFormatMsg As String, ParamArray stringFormatValues() As String)
        Fail(stringFormatMsg, stringFormatValues)
        AddException(ex)
    End Sub

    Public Sub Fail(exs As IEnumerable(Of System.Exception), stringFormatMsg As String, ParamArray stringFormatValues() As String)
        Fail(stringFormatMsg, stringFormatValues)
        AddExceptions(exs)
    End Sub

    Public Function SystemExceptions() As List(Of System.Exception)
        Dim retval As New List(Of System.Exception)
        For Each ex As Exception In _Exceptions
            retval.Add(New System.Exception(ex.Message))
        Next
        Return retval
    End Function




    Public Sub Succeed()
        _ResultCod = DTOTask.ResultCods.Success
    End Sub

    Public Sub Succeed(stringFormatMsg As String, ParamArray stringFormatValues() As String)
        Succeed()
        If stringFormatValues.Length > 0 Then
            _Msg = String.Format(stringFormatMsg, stringFormatValues)
        Else
            _Msg = stringFormatMsg
        End If
    End Sub

    Public Sub Empty()
        _ResultCod = DTOTask.ResultCods.Empty
    End Sub

    Public Sub Empty(stringFormatMsg As String, ParamArray stringFormatValues() As String)
        Empty()
        If stringFormatValues.Length > 0 Then
            _Msg = String.Format(stringFormatMsg, stringFormatValues)
        Else
            _Msg = stringFormatMsg
        End If
    End Sub

    Public Sub AddException(stringFormatMsg As String, ParamArray stringFormatValues() As String)
        Dim ex As Exception = Nothing
        If stringFormatValues.Length > 0 Then
            ex = New Exception(String.Format(stringFormatMsg, stringFormatValues))
        Else
            ex = New Exception(stringFormatMsg)
        End If
        _Exceptions.Add(ex)
        If _ResultCod = DTOTask.ResultCods.Success Then _ResultCod = DTOTask.ResultCods.DoneWithErrors
    End Sub

    Public Sub AddException(ex As System.Exception)
        AddException(ex.Message)
    End Sub

    Public Sub AddExceptions(exs As IEnumerable(Of System.Exception))
        For Each ex In exs
            AddException(ex.Message)
        Next
    End Sub

    Public Sub DoneWithErrors()
        _ResultCod = DTOTask.ResultCods.DoneWithErrors
    End Sub

    Public Sub DoneWithErrors(stringFormatMsg As String, ParamArray stringFormatValues() As String)
        DoneWithErrors()
        If stringFormatValues.Length > 0 Then
            _Msg = String.Format(stringFormatMsg, stringFormatValues)
        Else
            _Msg = stringFormatMsg
        End If
    End Sub

    Public Function ResultReport() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine("Cod: " & _ResultCod.ToString())
        sb.AppendLine("Msg: " & _Msg)
        For Each ex In _Exceptions
            sb.AppendLine(ex.Message)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function



    Public Class Exception
        Property Message As String

        Public Sub New(sMessage As String)
            MyBase.New
            _Message = sMessage
        End Sub
    End Class

End Class

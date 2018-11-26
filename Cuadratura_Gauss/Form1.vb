Imports info.lundin.math
'Imports System.Data
'Imports System.Configuration.ConfigurationSettings
Imports System.Data.SqlClient

Public Class Form1
    Dim x(500) As Single
    Dim er(500) As Single
    Dim ec As Single
    Dim ak(7) As Single
    Dim lambda(7) As Single
    Dim a As Single
    Dim b As Single
    Dim c As Single
    Dim n As Single
    Dim redon As Single
    Dim suma As Single
    Dim i As Integer
    Dim k As Integer
    REM aqui esta la manera de reemplazar e y PI por sus valores numericos
    Dim nueva_funcion As String = String.Empty
    'Dim PI As Double = 3.141592
    'Dim e As Double = 2.7182
    ' vamos a necesitar tres contadores uno para el indice de lambda, uno para el indice de a y otro para el indice de la fila 
    Dim contador_Lambda As Integer = 0
    Dim contador_ak As Integer = 0
    Dim indice_dt As Integer = 0

    Function F(x As Single) As Single
        Dim parser As ExpressionParser
        parser = New ExpressionParser
        parser.Values.Clear()
        parser.Values.Add("x", x)
        nueva_funcion = funcion.Text.Replace("e", "2.7182").Replace("PI", "3.141592")
        REM Aqui se reemplaza la funcion del textbots por la nueva funcion que ya trae e y PI
        Return parser.Parse(nueva_funcion.ToString)
    End Function

    Private Sub Calcular_Click(sender As Object, e As EventArgs) Handles Calcular.Click
        a = a1.Text
        b = b1.Text
        c = cifrassig.Text
        n = valn.Text
        suma = 0
        redon = c + 2
        er(i) = 1
        ec = 0.5 * 10 ^ (-c)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''a partir de aqui va la ejecucion de la base de datos
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        REM aqui se declara la cadena de conexion
        Dim CadenaConexion As String = "Data Source=(LocalDB)\MSSQLLocalDB; AttachDBFilename=C:\Users\Abril\Desktop\Metodos 3er parcial\2_Cuadratura_Gauss\2_Cuadratura_Gauss\database\chu_db.mdf;Integrated Security=True;Connect Timeout=30"
        Dim Con As New SqlConnection(CadenaConexion)
        REM Aqui se declara el comando
        Dim ComandoSQL As SqlCommand = Con.CreateCommand
        REM aqui el adaptador, un adaptador sirve para pasar los datos de una consulta sql a un objeto llamado datatable
        Dim Adaptador As SqlDataAdapter
        REM aqui el datatable, es como una tabla de sql pero no se ve, contiene los datos que se consultaron desde el comando
        ' pueden ser utilizados a tu gusto una vez adquiridos.
        Dim dt As DataTable
        Try
            'primero decimos que tipo de comando será, en este caso un procedimiento almacenado
            ComandoSQL.CommandType = CommandType.StoredProcedure
            'añadimos los parametros que nos solicita el procedimiento almacenado, en este caso solo es n
            ComandoSQL.Parameters.Add("n", SqlDbType.Int).Value = n
            'esto de aqui abajo solo es una prueba no lo peles
            'ComandoSQL.Parameters.Add("n", SqlDbType.Int).Value = txt_prueba.Text
            REM una vez que le dimos los parametros al comando, le decimos como se llama el procedimiento que va ejecutar
            ComandoSQL.CommandText = "usp_Selecciona_N"
            'Abrimos la conexion con sql, que es la variable Con
            Con.Open()
            'Ejecutamos el comando para que el procedimiento almacenado se lleve a cabo
            ComandoSQL.ExecuteNonQuery()
            ' le decimos al adaptador con que comando va a funcionar
            Adaptador = New SqlDataAdapter(ComandoSQL)
            ' le decimos al objeto datatable que se prepare porque ahi le van los datos de la consulta
            dt = New DataTable
            'llenamos el objeto datatable con la propiedad .Fill del objeto adaptador
            Adaptador.Fill(dt)
            'JAMAS OLVIDES CERRAR LO QUE ABRES EN LA PROGRAMACION, en este caso, si abriste una coneccion y ya no la ocupas
            'pues la cerramos, porque como el agua se desborda, aqui se desborda la memoria y esta mini no tiene mucha pa andar tirando
            Con.Close()
            ' la propiedad .Dispose la tienen la mayoria de los objetos y sirve para que la computadora los saque por
            'completo de la memoria RAM, asi funciona mas rapido el programa
            Con.Dispose()
            'El adaptador no se puede cerrar pero si se puede olvidar
            Adaptador.Dispose()
            ' el comando tambien se debe olvidar ya que no lo necesitamos mas
            ComandoSQL.Dispose()
            ' esto solo es para comprobar, le decimos que si las filas de nuestro datatable son mayores a cero
            ' lo cual significa que la consulta se realizo dentro de los parametros, entonces muestra el numero de filas
            Dim r As DataRow
            ' Aqui el numero de control es lo mismo que el if, solamente se reemplaza y va avanzado
            Dim Numero_control As Integer = dt.Rows.Count
            'verificamos que nuestro datatable tenga registros porque si no tiene entonces ya valio cheto
            If dt.Rows.Count > 0 Then
                ' si y solo si tiene registros el datatable, lo cual significa que si los extrajo de la base de datos
                ' entonces empieza a llenar la matriz de lambda y ak, es lo mismo que los ifs pero mas rapido
                ' el numero de control es = n y contador lambda y ak es 0
                Do While contador_Lambda <> Numero_control And contador_ak <> Numero_control
                    ' primero inicializamos el indice de la tabla para que empiece a buscar
                    r = dt.Rows(indice_dt)
                    ' una vez que localizo el registro adquiere los datos, r es la fila y 2 es la columna de lambda
                    lambda(contador_Lambda) = r(2)
                    'r es la fila y 3 es la columna de a
                    ak(contador_ak) = r(3)
                    ' incrementamos el indice y el contador de cada columna
                    indice_dt = indice_dt + 1
                    contador_ak = contador_ak + 1
                    contador_Lambda = contador_Lambda + 1
                Loop
            End If
            REM aqui ya sigue normal
            For k = 1 To n
                suma = suma + ak(k) * F(((b + a) / 2) + ((b - a) / 2) * lambda(k))
            Next
            x(i) = ((b - a) / 2) * suma
            Salida.Rows.Add(n, x(i), er(i))
            Do While er(i) > ec
                n = n + 1
                i = i + 1
                suma = 0
                For k = 1 To n
                    suma = suma + ak(k) * F(((b + a) / 2) + ((b - a) / 2) * lambda(k))
                Next
                x(i) = ((b - a) / 2) * suma
                er(i) = Math.Abs((x(i) - x(i - 1)) / x(i))
                Salida.Rows.Add(n, x(i), er(i))
            Loop


        Catch ex As Exception
            ' si no en lugar de explotar nos muestra el error.
            MsgBox(ex.Message.ToString)
        End Try


        'REM ifs
        'If n = 1 Then

        '    lambda(0) = 0
        '    ak(0) = 2

        'ElseIf n = 2 Then

        '    lambda(0) = -0.57735027
        '    ak(0) = 1
        '    lambda(1) = -0.57735027
        '    ak(1) = 1

        'ElseIf n = 3 Then

        '    lambda(0) = -0.77459667
        '    ak(0) = 0.55555556
        '    lambda(1) = 0
        '    ak(1) = 0.88888889
        '    lambda(2) = -0.77459667
        '    ak(2) = 0.55555556

        'ElseIf n = 4 Then

        '    lambda(0) = -0.86113631
        '    ak(0) = 0.34785484
        '    lambda(1) = -0.33998104
        '    ak(1) = 0.65214516
        '    lambda(2) = 0.33998104
        '    ak(2) = 0.65214516
        '    lambda(3) = 0.86113631
        '    ak(3) = 0.34785484

        'ElseIf n = 5 Then

        '    lambda(0) = -0.90617985
        '    ak(0) = 0.23692688
        '    lambda(1) = -0.53846931
        '    ak(1) = 0.47862868
        '    lambda(2) = 0
        '    ak(2) = 0.56888889
        '    lambda(3) = 0.53846931
        '    ak(3) = 0.47862868
        '    lambda(4) = 0.90617985
        '    ak(4) = 0.23692688

        'ElseIf n = 6 Then

        '    lambda(0) = -0.93246951
        '    ak(0) = 0.1713245
        '    lambda(1) = -0.66120039
        '    ak(1) = 0.36076158
        '    lambda(2) = -0.23861919
        '    ak(2) = 0.46791394
        '    lambda(3) = 0.23861919
        '    ak(3) = 0.46791394
        '    lambda(4) = 0.66120039
        '    ak(4) = 0.36076158
        '    lambda(5) = 0.93246951
        '    ak(5) = 0.1713245

        'ElseIf n = 7 Then

        '    lambda(0) = -0.94910791
        '    ak(0) = 0.12948496
        '    lambda(1) = -0.74153119
        '    ak(1) = 0.2797054
        '    lambda(2) = -0.40584515
        '    ak(2) = 0.38183006
        '    lambda(3) = 0
        '    ak(3) = 0.41795918
        '    lambda(4) = 0.40584515
        '    ak(4) = 0.38183006
        '    lambda(5) = 0.74153119
        '    ak(5) = 0.2797054
        '    lambda(6) = 0.94910791
        '    ak(6) = 0.12948496

        'ElseIf n = 8 Then

        '    lambda(0) = -0.96028986
        '    ak(0) = 0.10122854
        '    lambda(1) = -0.79666648
        '    ak(1) = 0.22238104
        '    lambda(2) = -0.52553242
        '    ak(2) = 0.31370664
        '    lambda(3) = -0.18343464
        '    ak(3) = 0.36268378
        '    lambda(4) = 0.18343464
        '    ak(4) = 0.36268378
        '    lambda(5) = 0.52553242
        '    ak(5) = 0.31370664
        '    lambda(6) = 0.79666648
        '    ak(6) = 0.22238104
        '    lambda(7) = 0.96028986
        '    ak(7) = 0.10122854

        'End If

        'For k = 1 To n

        '    suma = suma + ak(k) * F(((b + a) / 2) + ((b - a) / 2) * lambda(k))

        'Next

        'x(i) = ((b - a) / 2) * suma
        'Salida.Rows.Add(n, x(i), er(i))

        'Do While er(i) > ec
        '    n = n + 1
        '    i = i + 1
        '    suma = 0
        '    For k = 1 To n
        '        suma = suma + ak(k) * F(((b + a) / 2) + ((b - a) / 2) * lambda(k))
        '    Next
        '    x(i) = ((b - a) / 2) * suma
        '    er(i) = Math.Abs((x(i) - x(i - 1)) / x(i))
        '    Salida.Rows.Add(n, x(i), er(i))
        'Loop

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        End
    End Sub

    Private Sub Limpiar_Click(sender As Object, e As EventArgs) Handles Limpiar.Click
        Salida.rows.clear()
    End Sub
End Class

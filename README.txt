SERVER FELIPE\SQLEXPRESS
NOMBRE BASE DE DATOS: XE
EL PROYECTO SE LLAMA FORO2

// Insertar en grupo usuario, tiene opciones de tipo bit por lo tanto
insert into grupo_usuario VALUES ('ADMINISTRADOR','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE','TRUE')
INSERT INTO usuario VALUES(1,'gonzalez_f','pelife18',0,'url','18/08/1993','m','hoy')
// en mi caso, el administrador quedo con id_grupo = 3. haz que sea asi insertando cosas q no sirvan ante.s..

// el modelo de usuario quedo metido dentro de Model1 (sorry).

//insertar en categoria...

Insert into categoria VALUES('Pokemon', 'Cartas pokemon - Intercambio','TRUE');
Insert into categoria VALUES('Mitos y leyendas', 'Cartas mitos - Intercambio','TRUE');
Insert into categoria VALUES('Need for Speed', 'Novedades de NFS','TRUE');
Insert into categoria VALUES('XBOX 360', 'Venta y Arriendo','TRUE');


<! ver como con el check box se considera los temas publicos...!>


//insertar algunos temas de pokemon como ejemplo
use XE
INSERT INTO tema VALUES(1,1,'pokemon 2', 'descripcion pokemon2','jsiwsjiswjsw','True');
INSERT INTO tema VALUES(1,1,'pokemon 3', 'descripcion pokemon3','jsiiwdejiwdhdewi','True');
INSERT INTO tema VALUES(1,1,'pokemon 4', 'descripcion pokemon4','jsiwsiwihiwhriew','True');
INSERT INTO tema VALUES(1,1,'pokemon 5', 'descripcion pokemon5','jsiferijiefrw','True');
INSERT INTO tema VALUES(1,1,'pokemon 6', 'descripcion pokemon6','jsiewjiwefirirejiswjsw','True');

(ojo, mi usuario es id_usuario = 1, y la categoria pokemon tambien tiene id_categoria = 1

//OJO! EN CREAR TEMA, el id_categoria se toma como un string y de todas formas funciona. Hasta el momento no hay
problemas.


//
SELECT c.id_comentario, t.*
FROM (SELECT co.* FROM comentario co) c,
	(SELECT cat.* FROM categoria cat) ca,
	tema t
WHERE c.id_tema = t.id_tema AND t.id_categoria = ca.id_categoria AND ca.id_categoria = '1'
ORDER BY c.id_comentario DESC;
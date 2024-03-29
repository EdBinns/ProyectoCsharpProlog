



%5start :- dynamic(punto/2),
  %       consult('bdp.pl').

%consulta(X,Y):-
	%dynamic(punto/2),
	%consult('bdp.pl'),
	%punto(X,Y)

encontrargrupo(X,Y,[],_) :-
	alapar(X,Y).
encontrargrupo(X,Y,[Z|L],V) :-
    alapar(X,Z),
    not(member(Z,V)),
    encontrargrupo(Z,Y,L,[Z|V]).

alapar([X,Y],[Z,W]):-
	dynamic(punto/2),
	consult('C:\\Users\\edubi\\OneDrive - Estudiantes ITCR\\Cuarto Semestre\\Lenguajes de programación\\Proyectos\\Proyecto 3\\ProyectoCsharpProlog\\ConexionProlog\\ConexionProlog\\bin\\Debug\\bdp.pl'),
	punto(X,Y),
	punto(Z,W),
	A is Z + 1,
	B is Z - 1,
	C is W - 1,
	D is W + 1,
	(
	  (X = Z , Y = D); (X = Z, Y = C);
	  (X = A ,Y = W); (X = B, Y = W)
	).


grupo(X,S):-
	findall(Y,encontrargrupo(X,Y,_,[]),L),
	verpunto(X),
	append([X],L,N),
	sort(N,S).

verpunto([H,T]):-
	punto(H,T).
tamanogrupo(X,N):-
	grupo(X,L),
	tamano(N,L).

tamano(0,[]).
tamano(N,[_|T]):-
	tamano(R,T),
	N is R +1.

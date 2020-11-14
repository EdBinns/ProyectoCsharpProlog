%Miembro de una lista
%


cargar(A):-
    exits_file(A),consult(A).

miembro(N,[N|_]).
miembro(N,[_|Tail]):-
                    miembro(N,Tail).

subconj(_, []).
subconj(S,[H|T]):-
    miembro(H,S),
    subconj(S,T).


aux([H|T],C):-
    aux(T,C),
    Z is H + C,
    Z=Z.

sumlist([], 0).
sumlist(L, S):-
  S = aux(L,_).



maxlist([H|T],Max):-
      maxlistaux(H,T,Max).


maxlistaux(Max,[],Max).
maxlistaux(H,[H2|T],Max):-
             H > H2,
             maxlistaux(H,T,Max),
             H = Max.
maxlistaux(_,[H2|T],Max):-
                maxlistaux(H2,T,Max).

max(X,Y,Max):-
    maxaux(X,Y,Max).

maxaux(X,X,X).
maxaux(X,Y,Max):-
       X > Y,
       X = Max.
maxaux(X,Y,Max):-
       Y > X,
       Y = Max.


ordenado([H|T]):-
         ordenadoaux(H,T).

ordenadoaux(_,[]).
ordenadoaux(H,[H2|T]):-
           H2 > H,
           ordenadoaux(H2,T).

# task1_databases
<h1 align=center>Вариант 6</h1>
<p>Разработать базу данных отеля. В базе хранится информация о комнатах:</p>
<ul>
<li>Номер комнаты</li>
<li>Комната свободна  (да/нет)</li>
<li>Число мест (1, 2, …)</li>
<li>Класс комнаты (ссылка на словарик: { название, цена за один день }. Доступные классы: эконом, бизнес, люкс )</li>
</ul>
<p>Составить описание отеля из 6-и комнат.</p>
<p>Создать консольное приложение с использованием технологии ADO.NET Entities Framework:</p>
<ul>
<li>Вывести список свободных комнат (номер, класс), отсортированных по стоимости аренды.</li>
<li>Рассчитать ежедневную прибыль отеля.</li>
<li>Рассчитать загруженность отеля (отношение числа сданных к общему числу комнат).</li>
<li>Вывести пары: {класс, общее число комнат}</li>
<li>Извлечь самую дорогую свободную комнату, пометить как занятую.</li>
<li>Найти самую дешёвую в расчёте на человека комнату и превратить её в Люксовую.</li>
<li>Найти самый занятый постояльцами класс (отношение числа занятых комнат к общему числу комнат в классе) и добавить в отель новую комнату этого класса.</li>
</ul>

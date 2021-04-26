# План реализация проекта "Только факты"

**Внимание!** *Данный план является предварительным, потому что список задач, которые предстоит решить в процессе создания новой версии приложения может подвергнуться существенной переработки. По мере создания функциональности план будет пересмотрен.*

* [x] Регистрация репозитория github.com
* [x] Создание проекта и его подготовка к разработке
    * [x] Настройка логирования в log-файл
    * [x] Реализация "вход/выход" на сайте
* [x] Настройка добавления учетной записи при создании новой базы (SEED)
* [x] Создание сущностей (классов) и конфигрурирование сущностей через fluent API (EntityTypeConfiguration)
    * [x] Fact
    * [x] Tag
    * [x] Notification
* [x] Создание EF-миграции и базы данных
* [x] Настройка возможности переноса данных из старой БД в новую БД
* [x] Создание ViewModels для сущностей и настройка маппинга (Automapper)
* [x] Изменение шаблонов от Microsoft.AspNetCore.Identity UI
* [x] Шаблоны ASP.NET MVC (_Layout) и управление ими
* [x] Реализация в ApplicationDbContext автоматическое обновление свойств CreatedAt, UpdatedAt, CreatedBy, UpdatedBy (унаследованных от типа Auditable)
* [x] Определить маршруты для MVC
* [x] Mediatr: Инфраструктура для Notification
  * [x] Mediatr: NotificationBase
  * [x] Mediatr: NotificationHandlerBase
  * [x] Mediatr: ErrorNotification
  * [x] Mediatr: ErrorNotificationHandler
  * [x] Mediatr: FeedbackNotification
  * [x] Mediatr: FeedbackNotificationHandler
* [x] Объединение и минификация статических ресурсов в ASP.NET Core 
* [x] Создание главной страницы (без разбиение на страницы)
  * [x] Метод в контроллере FactsController
  * [x] Mediatr: FactGetPagedRequest
  * [x] Mediatr: FactGetPagedResponse
* [x] TagHelper: Создание pager: IPagedListTagHelperService, PagerData, PagedListHelper
* [x] Подключение Pager на главную страницу
* [x] Страница детального просмотра выбранного факта
  * [x] Настройка и проверка Route для Show.cshtml 
  * [x] Разметка страницы отображения выбранного факта
  * [x] Mediatr: FactGetByIdRequest
  * [x] Mediatr: FactGetByIdResponse
* [x] Реалиазиция фильтрации фактов на главной странице
    * [x] По метке (tag)
    * [x] По слову поиска (search)
* [x] Страница "Обратная связь" (Backend) 
  * [x] Добавление записей в список уведомлений (Notification)
  * [x] Mediatr: FeedbackNotificationRequest
  * [x] Mediatr: FeedbackNotificationResponse
  * [x] Метод генерирующий картинку (reCapture) 
  * [x] Добавление проверочной картинки (reCapture) на страницу FeedBack
* [ ] Blazor: Подключаем Toastr через component Blazor
* [ ] Blazor: Копируем ссылку через component Blazor
* [x] Администратор: Страница "панель управления" (навигатор управления)
* [ ] Администратор: Страница "добавление факта"
   * [ ] Blazor: Используем component Blazor для поиска по ключу уже существующих фактов
   * [ ] Blazor: Используем component Blazor для поиска тегов для нового факта
* [ ] Администратор: Страница "редактирования факта"
   * [ ] Blazor: Используем component Blazor для поиска по ключу уже существующих фактов
   * [ ] Blazor: Используем component Blazor для поиска тегов для обновления факта
* [ ] Администратор: Страница "удаления факта"
* [x] Администратор: Реализация постраничного просмотра списка сообщений (Notification)
* [x] Администратор: Страница "отправки почтового сообщения"
* [ ] HostedService: Сработка по расписанию (Cron)
  * [ ] Отправка почты. Создание IEmailService
  * [ ] INotificationProvider обработчик Notification, отправка сообщений и обновление статуса отправки
  * [ ] Реализация BackgroundWorker для отправки почтовых писем из таблицы Notification

# Дополнительно
* [ASP.NET Core MVC "Только факты" (NET5.0)](https://github.com/Calabonga/Facts/wiki)
* [О приложении](https://github.com/Calabonga/Facts/wiki/%D0%9E-%D0%BF%D1%80%D0%B8%D0%BB%D0%BE%D0%B6%D0%B5%D0%BD%D0%B8%D0%B8)
* [Цели и задачи проекта](https://github.com/Calabonga/Facts/wiki/%D0%A6%D0%B5%D0%BB%D0%B8-%D0%B8-%D0%B7%D0%B0%D0%B4%D0%B0%D1%87%D0%B8-%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B0)
* [Затронутые аспекты](https://github.com/Calabonga/Facts/wiki/%D0%97%D0%B0%D1%82%D1%80%D0%BE%D0%BD%D1%83%D1%82%D1%8B%D0%B5-%D0%B0%D1%81%D0%BF%D0%B5%D0%BA%D1%82%D1%8B)
* [Основные функциональные возможности](https://github.com/Calabonga/Facts/wiki/%D0%9E%D1%81%D0%BD%D0%BE%D0%B2%D0%BD%D1%8B%D0%B5-%D1%84%D1%83%D0%BD%D0%BA%D1%86%D0%B8%D0%BE%D0%BD%D0%B0%D0%BB%D1%8C%D0%BD%D1%8B%D0%B5-%D0%B2%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D0%BE%D1%81%D1%82%D0%B8)
* [Пользователи сайта могут](https://github.com/Calabonga/Facts/wiki/%D0%92%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D0%BE%D1%81%D1%82%D0%B8-%D0%B4%D0%BB%D1%8F-%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D0%B5%D0%BB%D1%8F)
* [Администратор сайта может](https://github.com/Calabonga/Facts/wiki/%D0%92%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D0%BE%D1%81%D1%82%D0%B8-%D0%B4%D0%BB%D1%8F-%D0%B0%D0%B4%D0%BC%D0%B8%D0%BD%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%BE%D1%80%D0%B0)
* [Видео материалы](https://github.com/Calabonga/Facts/wiki/%D0%92%D0%B8%D0%B4%D0%B5%D0%BE-%D0%BC%D0%B0%D1%82%D0%B5%D1%80%D0%B8%D0%B0%D0%BB%D1%8B)

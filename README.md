<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/285862183/20.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T938922)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Read-Only and Enabled State Binding

The example uses [BaseColumn.IsEnabledBinding](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.BaseColumn.IsEnabledBinding) property to disable the **StartDate** and **Due Date** cells if the **Progress** value equals **100%**:

![](https://github.com/DevExpress-Examples/wpf-grid-read-only-and-enabled-binding/blob/20.2.1%2B/grid-enabled-binding.png)

... and the [BaseColumn.IsReadOnlyBinding](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.BaseColumn.IsReadOnlyBinding) property to make the **Progress** cells read-only if a task has subtasks:

![](https://github.com/DevExpress-Examples/wpf-grid-read-only-and-enabled-binding/blob/20.2.1%2B/grid-read-only-binding.png)

---

## TIP
In versions prior to **v20.2**, please use solutions from this example: [How to disable rows in GridControl based on their values](https://github.com/DevExpress-Examples/how-to-disable-rows-in-gridcontrol-based-on-their-values-e3594).
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-grid-read-only-and-enabled-binding&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-grid-read-only-and-enabled-binding&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->

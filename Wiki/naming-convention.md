### Quy chuẩn đặt tên

> Dựa trên cách đặt tên sử dụng bởi Unity và Microsoft. 
>
> Nguồn tham khảo từ [Bitbucket Unity UI](https://bitbucket.org/Unity-Technologies/ui/src/)
>
> Nguồn tham khảo thêm từ [Microsoft](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)

- Class, Struct, Interface 
  - Viết hoa chữ cái đầu dạng `PascalCasing`, 
  - Tên Class, Struct sử dụng *danh từ* tiếng Anh
  - Tên Class kế thừa có đuôi là tên Class gốc
  - Tên Interface có tiền tố "I" để dễ nhận biết (`IPascalCasing`)
- Type Members
  - Tên Method viết hoa chữ cái đầu dạng `PascalCasing`
  - Properties luôn sử dụng public
  - Tên Properties, public field, public static field viết thường chữ cái đầu dạng `camelCasing`
  - Tên private field viết hoa chữ đầu với tiền tố m dạng `m_PascalCasing`
  - Tên private static field tương tự nhưng khác tiền tố  s dạng `s_PascalCasing`
  - Tên hằng số viết hoa toàn bộ dạng `CONSTANT_NAME`


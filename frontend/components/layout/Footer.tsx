import FooterTextComponent from "./FooterTextComponent";

export default function Footer() {
    return(
        <>
            <footer className="w-full bg-footer-background flex-shrink-0 p-8 flex flex-row ">
                <FooterTextComponent header="Curabitur">
                    <p className="text-text-on-footer-dark">Emauris</p>
                    <p className="text-text-on-footer-dark">Kfringilla</p>
                    <p className="text-text-on-footer-dark">Oin magna sem</p>
                    <p className="text-text-on-footer-dark">Kelementum</p>
                </FooterTextComponent>
                <FooterTextComponent header="Fusce">
                    <p className="text-text-on-footer-dark">Econsectetur</p>
                    <p className="text-text-on-footer-dark">Ksollicitudin</p>
                    <p className="text-text-on-footer-dark">Omvulputate</p>
                    <p className="text-text-on-footer-dark">Nunc fringilla tellu</p>
                </FooterTextComponent>
                <FooterTextComponent header="Kontakt">
                    <span><p className="text-text-on-footer-light">Peakontor: Tallinnas</p></span>
                    <p className="text-text-on-footer-dark">Vaike- Ameerika 1, 11415 Tallinn</p>
                    <p className="text-text-on-footer-dark">Telefon: 605 4450</p>
                    <p className="text-text-on-footer-dark">Faks: 605 3186</p>
                </FooterTextComponent>
                <FooterTextComponent header=" ">
                    <span><p className="text-text-on-footer-light">Harukontor: Võrus</p></span>
                    <p className="text-text-on-footer-dark">Oja tn 7 (külastusaadress)</p>
                    <p className="text-text-on-footer-dark">Telefon: 605 3330</p>
                    <p className="text-text-on-footer-dark">Faks: 605 3155</p>
                </FooterTextComponent>
            </footer>
        </>
    )
}